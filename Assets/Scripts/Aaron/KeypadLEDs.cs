using UnityEngine;
using TMPro; // Librería base que contiene TMP_Text, TextMeshPro y TextMeshProUGUI
using System.Collections.Generic;

public class KeypadLEDs : MonoBehaviour
{
    public SerialManager gestorSerial;

    [Header("Textos de Pistas (Acepta Canvas UGUI y Textos 3D)")]
    [Tooltip("Orden físico: Azul, Blanco, Amarillo, Verde, Rojo")]
    // Al usar TMP_Text, Unity acepta ambos tipos de componentes en el mismo arreglo
    public TMP_Text[] textosPistasColores = new TMP_Text[5];

    [Header("UI del Keypad")]
    public TextMeshProUGUI displayInput;
    public GameObject panelKeypad;

    private string solucionCorrecta = "";

    void Start()
    {
        panelKeypad.SetActive(false);
        GenerarPuzzleNuevo();
    }

    public void GenerarPuzzleNuevo()
    {
        solucionCorrecta = "";
        for (int i = 0; i < 5; i++)
        {
            int num = Random.Range(0, 10);
            solucionCorrecta += num.ToString();

            // Verificamos que no esté vacío antes de asignarle el número
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
            panelKeypad.SetActive(false); // Cierra el keypad al ganar
        }
        else
        {
            gestorSerial.EnviarComandoArduino("LED_FAIL");
            Borrar(); // Borra el input para volver a intentar
        }
    }
}