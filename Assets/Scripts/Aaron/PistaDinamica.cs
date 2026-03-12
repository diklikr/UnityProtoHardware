using UnityEngine;
using TMPro;

public class PistaDinamica : MonoBehaviour
{
    public TextMeshProUGUI cuadroTexto;
    public string encabezado = "NOTA DE SEGURIDAD:";
    public bool esPistaA; // Si es True, muestra switches 1,3,5. Si es False, 2,4.

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string clave = SerialManager.claveSwitches; // Obtenida del SerialManager
            if (clave.Length < 5) return;

            if (esPistaA)
                cuadroTexto.text = encabezado + "\nInterruptores 1,3,5 en: " + clave[0] + ", " + clave[2] + ", " + clave[4];
            else
                cuadroTexto.text = encabezado + "\nRelés 2 y 4 en: " + clave[1] + ", " + clave[3];

            cuadroTexto.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) => cuadroTexto.gameObject.SetActive(false);
}