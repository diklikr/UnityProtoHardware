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
    private string estadoAnteriorSwitches = "";
    public static string codigoMaestro = "0000";
    public static int puzzleActual = 0;
    public static event System.Action<int> AlRecibirInputIR;
    public Door scriptPuerta;

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
                string dato = puerto.ReadLine().Trim();

                if (dato.StartsWith("SYS:"))
                {
                    string[] partes = dato.Substring(4).Split(',');

                    // VALIDACIÓN: Solo intentamos leer si llegaron exactamente las 6 partes esperadas
                    if (partes.Length >= 8)
                    {
                        // int.TryParse intenta convertir. Si falla (por basura en el cable), no crashea el juego.
                        int.TryParse(partes[0], out p1);
                        int.TryParse(partes[1], out p7);
                        int.TryParse(partes[2], out p2);
                        int.TryParse(partes[3], out p6);
                        int.TryParse(partes[4], out btnInaki);
                        int.TryParse(partes[4], out btnInaki);
                        claveSwitches = partes[5];

                        // --- Fix: Detectar cambios e imprimir en consola ---
                        if (claveSwitches != estadoAnteriorSwitches)
                        {
                            // Evitamos que imprima en el instante 0 al arrancar el juego
                            if (estadoAnteriorSwitches != "")
                            {
                                Debug.Log("🔌 Switches físicos cambiaron a: " + claveSwitches);
                            }
                            estadoAnteriorSwitches = claveSwitches; // Guardamos el nuevo estado
                        }
                        codigoMaestro = partes[6];
                        int.TryParse(partes[7], out puzzleActual);
                    }
                    else
                    {
                        // Si llega una cadena cortada, la ignoramos y avisamos en consola
                        Debug.LogWarning("Cadena incompleta ignorada: " + dato);
                    }
                }
                else if (dato.StartsWith("CMD:"))
                {
                    string cmd = dato.Substring(4);
                    if (cmd == "CAM_NEXT") scriptCamaras.SwitchToNextCamera();
                    else if (cmd == "CAM_PREV") scriptCamaras.SwitchToPreviousCamera();
                    else if (cmd == "ZOOM_IN") scriptCamaras.ZoomIn();
                    else if (cmd == "ZOOM_OUT") scriptCamaras.ZoomOut();
                    // Ültima adición, comando para abrir la puerta del búnker al ganar el juego además de imprimir en consola
                    else if (cmd == "MASTER_WIN")
                    {
                        Debug.Log("¡VICTORIA TOTAL! Abriendo la puerta del búnker...");
                        if (scriptPuerta != null)
                        {
                            scriptPuerta.OpenDoor();
                        }
                    }
                    
                    // NUEVO: Procesar los números del control
                    else if (cmd.StartsWith("IR_"))
                    {
                        if (int.TryParse(cmd.Substring(3), out int numeroDetectado))
                        {
                            AlRecibirInputIR?.Invoke(numeroDetectado);
                        }
                    }
                }
            }
            catch (System.TimeoutException)
            {
                // Es normal que haya timeouts en Serial, los ignoramos
            }
            catch (System.Exception e)
            {
                // Atrapamos cualquier otro error raro para que Unity no congele el juego
                Debug.LogWarning("Error leyendo puerto: " + e.Message);
            }
        }
    }

    public void EnviarComandoArduino(string c) { if (puerto.IsOpen) puerto.WriteLine(c + "\n"); }
}