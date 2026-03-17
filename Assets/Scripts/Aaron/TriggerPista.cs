using UnityEngine;


public class TriggerPista : MonoBehaviour
{
    public GameObject canvasPista; // Arrastra aquí el UI Text de la pista correspondiente

    public AudioSource pistaAudioOpen; // Arrastra aquí el audio de la pista correspondiente

    public AudioSource pistaAudioClose;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasPista.SetActive(true);

            pistaAudioOpen.Play();
        }



    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasPista.SetActive(false);
            pistaAudioClose.Play();
        }

    }
}