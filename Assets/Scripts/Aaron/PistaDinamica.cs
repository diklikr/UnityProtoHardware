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

            // Traducimos el 1 o 0 a un texto más inmersivo con colores de interfaz
            string s1 = clave[0] == '1' ? "<color=#00FF00>ACTIVO</color>" : "<color=#FF0000>INACTIVO</color>";
            string s2 = clave[1] == '1' ? "<color=#00FF00>ACTIVO</color>" : "<color=#FF0000>INACTIVO</color>";
            string s3 = clave[2] == '1' ? "<color=#00FF00>ACTIVO</color>" : "<color=#FF0000>INACTIVO</color>";
            string s4 = clave[3] == '1' ? "<color=#00FF00>ACTIVO</color>" : "<color=#FF0000>INACTIVO</color>";
            string s5 = clave[4] == '1' ? "<color=#00FF00>ACTIVO</color>" : "<color=#FF0000>INACTIVO</color>";

            // Actualizamos el texto dependiendo de qué parte de la pista sea
            if (esPistaA)
            {
                cuadroTexto.text = "<b>" + encabezado + "</b>\n\n" +
                                   "Aviso al personal de mantenimiento. Las terminales impares han sido recalibradas tras el último incidente.\n" +
                                   "Para evitar una sobrecarga en el núcleo, mantengan la consola en esta configuración:\n\n" +
                                   "  • Bomba Principal (1): " + s1 + "\n" +
                                   "  • Filtro de Aire (3): " + s3 + "\n" +
                                   "  • Extractor (5): " + s5 + "\n\n" +
                                   "Cualquier modificación sin autorización provocará el bloqueo de la zona.";
            }
            else
            {
                cuadroTexto.text = "<b>" + encabezado + "</b>\n\n" +
                                   "Directiva de emergencia. Las líneas pares sufrieron daños durante el apagón.\n" +
                                   "Hasta que el jefe de mecánicos regrese, el soporte vital requiere estrictamente los siguientes parámetros:\n\n" +
                                   "  • Refrigeración (2): " + s2 + "\n" +
                                   "  • Purificador (4): " + s4 + "\n\n" +
                                   "No intenten puentear los fusibles o el cuarto de máquinas entero estallará.";
            }

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