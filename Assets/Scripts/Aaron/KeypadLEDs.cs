using UnityEngine;
using TMPro; // Librería base que contiene TMP_Text, TextMeshPro y TextMeshProUGUI

public class KeypadLEDs : MonoBehaviour
{
    public SerialManager gestorSerial;

    [Header("Textos de Pistas (Acepta Canvas UGUI y Textos 3D)")]
    [Tooltip("Orden físico estricto: Azul, Blanco, Amarillo, Verde, Rojo")]
    public TMP_Text[] textosPistasColores = new TMP_Text[5];

    [Header("UI del Keypad")]
    public TextMeshProUGUI displayInput;
    public CanvasGroup grupoCanvasKeypad;

    private string solucionCorrecta = "";

    void Start()
    {
        CerrarKeypad(); // Se oculta al iniciar el juego para no estorbar
        GenerarPuzzleNuevo();
    }

    public void GenerarPuzzleNuevo()
    {
        solucionCorrecta = "";
        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(0, 10);
            solucionCorrecta += num.ToString();

            // Verificamos que el texto esté asignado en el inspector antes de escribirle
            if (textosPistasColores[i] != null)
            {
                textosPistasColores[i].text = num.ToString();
            }
        }
    }

    // Se llama desde cada botón numérico (0-9) en el Canvas
    public void ClickBoton(string n)
    {
        if (displayInput.text.Length < 5) displayInput.text += n;
    }

    // Se llama desde el botón "Delete" o "Borrar"
    public void Borrar() => displayInput.text = "";

    // Se llama desde el botón "OK" o "Enter"
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
            Borrar(); // Borra el input para que el jugador lo vuelva a intentar
        }
    }

    // --- MÉTODOS PÚBLICOS PARA MOSTRAR/OCULTAR EL CANVAS GROUP ---
    // Giovanni puede llamar a AbrirKeypad() desde el Raycast cuando el NPC llegue a la terminal

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