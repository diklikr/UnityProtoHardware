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
            Debug.Log("Player entered the trigger area. Canvas activated and audio played.");
        }



    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasPista.SetActive(false);
            pistaAudioClose.Play();
            Debug.Log("Player exited the trigger area. Canvas deactivated and audio played.");
        }

    }
}