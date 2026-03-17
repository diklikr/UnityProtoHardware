using UnityEngine;
using TMPro;

public class TerminalRemota : MonoBehaviour
{
    public SerialManager gestorSerial;

    [Header("UI y Pistas")]
    public TMP_Text[] textosPistasColores = new TMP_Text[5];
    public TextMeshProUGUI displayInput;
    public CanvasGroup grupoCanvas;

    private string solucionCorrecta = "";
    private string inputActual = "";

    void Start()
    {
        CerrarTerminal();
        GenerarPuzzleNuevo();
        // Nos suscribimos para escuchar al control remoto de Iñaki
        SerialManager.AlRecibirInputIR += ProcesarBotonIR;
    }

    void OnDestroy()
    {
        // Nos desuscribimos al destruir el objeto para evitar errores
        SerialManager.AlRecibirInputIR -= ProcesarBotonIR;
    }

    public void GenerarPuzzleNuevo()
    {
        solucionCorrecta = "";
        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(0, 10);
            solucionCorrecta += num.ToString();
            if (textosPistasColores[i] != null) textosPistasColores[i].text = num.ToString();
        }
    }

    private void ProcesarBotonIR(int numeroBoton)
    {
        // Si el Canvas no está visible (jugador lejos), ignoramos el control IR
        if (grupoCanvas.alpha == 0f) return;

        if (numeroBoton == 14) // Comando de Borrar
        {
            if (inputActual.Length > 0)
                inputActual = inputActual.Substring(0, inputActual.Length - 1);
        }
        else if (numeroBoton >= 0 && numeroBoton <= 9)
        {
            if (inputActual.Length < 5)
                inputActual += numeroBoton.ToString();
        }

        displayInput.text = inputActual;

        // Validar automáticamente al llegar a 5 dígitos
        if (inputActual.Length == 5)
        {
            if (inputActual == solucionCorrecta)
            {
                gestorSerial.EnviarComandoArduino("LED_WIN");
                CerrarTerminal();
            }
            else
            {
                gestorSerial.EnviarComandoArduino("LED_FAIL");
                inputActual = "";
                displayInput.text = "";
            }
        }
    }

    public void AbrirTerminal()
    {
        grupoCanvas.alpha = 1f;
    }

    public void CerrarTerminal()
    {
        grupoCanvas.alpha = 0f;
        inputActual = ""; // Se limpia al alejarse
        if (displayInput != null) displayInput.text = "";
    }
}