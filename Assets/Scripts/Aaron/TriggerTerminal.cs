using UnityEngine;

public class TriggerTerminal : MonoBehaviour
{
    [Header("Referencia al Keypad")]
    [Tooltip("Arrastra aquí el objeto del Canvas que tiene el script KeypadLEDs")]
    public TerminalRemota scriptKeypad;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró a la zona es el NPC (debe tener el tag "Player")
        if (other.CompareTag("Player"))
        {
            scriptKeypad.AbrirTerminal();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el jugador hace clic en otro lado y el NPC se aleja, el keypad se cierra solo
        if (other.CompareTag("Player"))
        {
            scriptKeypad.CerrarTerminal();
        }
    }
}