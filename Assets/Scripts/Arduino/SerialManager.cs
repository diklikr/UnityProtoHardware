using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour
{
    SerialPort puerto;
    public string puertoCOM = "COM10";
    public CamaraSwitch scriptCamaras;

    // Datos estáticos para que otros scripts los lean
    public static int p1, p7, p2, p6, btnInaki;
    public static string claveSwitches = "00000";

    void Start()
    {
        puerto = new SerialPort(puertoCOM, 9600);
        puerto.ReadTimeout = 10;
        try { puerto.Open(); Debug.Log("Puerto Serial " + puertoCOM + " abierto con éxito."); } catch (System.Exception e) { Debug.LogError(e.Message); }
    }

    void Update()
    {
        if (puerto != null && puerto.IsOpen)
        {
            try
            {
                string dato = puerto.ReadLine();

                // Muestra en consola lo que llega del Arduino:
                Debug.Log("Recibiendo: " + dato);

                if (dato.StartsWith("SYS:"))
                {
                    string[] partes = dato.Substring(4).Split(',');
                    p1 = int.Parse(partes[0]);
                    p7 = int.Parse(partes[1]);
                    p2 = int.Parse(partes[2]);
                    p6 = int.Parse(partes[3]);
                    btnInaki = int.Parse(partes[4]);
                    claveSwitches = partes[5]; // Aquí llega el "01010" por ejemplo
                }
                else if (dato.StartsWith("CMD:"))
                {
                    string cmd = dato.Substring(4);

                    // Para confirmar que Unity sí está leyendo el comando limpio
                    Debug.Log("Comando recibido limpio: [" + cmd + "]");

                    if (cmd == "CAM_NEXT") scriptCamaras.SwitchToNextCamera();
                    else if (cmd == "CAM_PREV") scriptCamaras.SwitchToPreviousCamera();
                    else if (cmd == "ZOOM_IN") scriptCamaras.ZoomIn();
                    else if (cmd == "ZOOM_OUT") scriptCamaras.ZoomOut();
                }
            }
            catch (System.TimeoutException) { }
        }
    }

    public void EnviarComandoArduino(string c) { if (puerto.IsOpen) puerto.WriteLine(c + "\n"); }
}