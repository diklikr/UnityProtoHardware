using UnityEngine;
using TMPro;

public class KeypadLEDs : MonoBehaviour
{
    public SerialManager gestorSerial;

    [Header("Textos de Pistas (Acepta Canvas UGUI y Textos 3D)")]
    public TMP_Text[] textosPistasColores = new TMP_Text[5];

    [Header("UI del Keypad")]
    public TextMeshProUGUI displayInput;
    public CanvasGroup grupoCanvasKeypad; // Reemplaza al GameObject

    private string solucionCorrecta = "";

    void Start()
    {
        CerrarKeypad(); // Se oculta al iniciar
        GenerarPuzzleNuevo();
    }

    public void GenerarPuzzleNuevo()
    {
        solucionCorrecta = "";
        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(0, 10);
            solucionCorrecta += num.ToString();

            if (textosPistasColores[i] != null)
            {
                textosPistasColores[i].text = num.ToString();
            }
        }
    }

    public void ClickBoton(string n)
    {
        if (displayInput.text.Length < 5) displayInput.text += n;
    }

    public void Borrar() => displayInput.text = "";

    public void Validar()
    {
        if (displayInput.text == solucionCorrecta)
        {
            gestorSerial.EnviarComandoArduino("LED_WIN");
            CerrarKeypad(); // Se oculta cuando ganan
        }
        else
        {
            gestorSerial.EnviarComandoArduino("LED_FAIL");
            Borrar();
        }
    }

    // --- MÉTODOS PÚBLICOS PARA MOSTRAR/OCULTAR EL CANVAS GROUP ---

    public void AbrirKeypad()
    {
        if (grupoCanvasKeypad != null)
        {
            grupoCanvasKeypad.alpha = 1f;
            grupoCanvasKeypad.interactable = true;
            grupoCanvasKeypad.blocksRaycasts = true;
        }
    }

    public void CerrarKeypad()
    {
        if (grupoCanvasKeypad != null)
        {
            grupoCanvasKeypad.alpha = 0f;
            grupoCanvasKeypad.interactable = false;
            grupoCanvasKeypad.blocksRaycasts = false;
        }
    }
}