using UnityEngine;
using TMPro;

public class PistaDinamica : MonoBehaviour
{
    public TextMeshProUGUI cuadroTexto;

    [Header("Control Visual")]
    public CanvasGroup grupoCanvas; // Arrastrar aquí el objeto que tiene el CanvasGroup

    public string encabezado = "NOTA DE SEGURIDAD:";
    public bool esPistaA; // True para pista 1,3,5. False para pista 2,4.

    void Start()
    {
        // Nos aseguramos de que inicie oculto y sin estorbar los clics
        if (grupoCanvas != null)
        {
            grupoCanvas.alpha = 0f;
            grupoCanvas.interactable = false;
            grupoCanvas.blocksRaycasts = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string clave = SerialManager.claveSwitches;
            if (clave.Length < 5) return;

            // Actualizamos el texto
            if (esPistaA)
                cuadroTexto.text = encabezado + "\nInterruptores 1,3,5 en: " + clave[0] + ", " + clave[2] + ", " + clave[4];
            else
                cuadroTexto.text = encabezado + "\nRelés 2 y 4 en: " + clave[1] + ", " + clave[3];

            // Hacemos visible el CanvasGroup
            if (grupoCanvas != null)
            {
                grupoCanvas.alpha = 1f;
                grupoCanvas.interactable = true;
                grupoCanvas.blocksRaycasts = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ocultamos el CanvasGroup al alejarnos
            if (grupoCanvas != null)
            {
                grupoCanvas.alpha = 0f;
                grupoCanvas.interactable = false;
                grupoCanvas.blocksRaycasts = false;
            }
        }
    }
}