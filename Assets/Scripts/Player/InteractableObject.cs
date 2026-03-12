using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("¡Interactuaste con: " + gameObject.name + "!");
        // Aquí podrías destruir el objeto o abrir un inventario
    }
}