using UnityEngine;

public class TriggerPista : MonoBehaviour
{
    public GameObject canvasPista; // Arrastra aquí el UI Text de la pista correspondiente

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) canvasPista.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canvasPista.SetActive(false);
    }
}