using UnityEngine;

public enum ObjectType { Puerta, Cofre, Nota, Item }

public class InteractableObject : MonoBehaviour
{
    [SerializeField] LayerMask InteractableMask;
    [SerializeField] ObjectType tipoDeObjeto; // Eliges el tipo en el Inspector

    // Referencia al script que maneja la UI
    [SerializeField] UIManager uiManager;

    void OnMouseDown()
    {
        // Tu lógica de máscara de capa actual...
        if (InteractableMask == (InteractableMask | (1 << gameObject.layer)))
        {
            // Llamamos a la UI pasando el tipo de objeto
            uiManager.MostrarCanvasSegunTipo(tipoDeObjeto);
        }
    }
}

