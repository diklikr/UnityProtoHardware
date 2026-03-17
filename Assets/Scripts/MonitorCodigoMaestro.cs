using UnityEngine;
using TMPro;

public class MonitorCodigoMaestro : MonoBehaviour
{
    [Tooltip("Arrastra aquí el componente TextMeshPro UGUI de tu Canvas")]
    public TextMeshProUGUI textoDisplay;

    void Update()
    {
        // Evitar errores si el Arduino aún no envía datos
        if (string.IsNullOrEmpty(SerialManager.codigoMaestro) || SerialManager.codigoMaestro.Length < 4)
            return;

        string cadenaMostrar = "";

        // Revisamos los 4 dígitos
        for (int i = 0; i < 4; i++)
        {
            // Si el índice es menor a los puzzles resueltos, mostramos el número real
            if (i < SerialManager.puzzleActual)
            {
                cadenaMostrar += SerialManager.codigoMaestro[i] + " ";
            }
            else // Si no, mostramos un guion bajo (o puedes cambiarlo por un asterisco)
            {
                cadenaMostrar += "_ ";
            }
        }

        // Imprime el resultado final (Ej: "8 _ _ _" o "8 2 5 _")
        textoDisplay.text = cadenaMostrar.Trim();
    }
}