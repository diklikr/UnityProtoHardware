using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Paneles de UI")]
    public GameObject panelPuerta;
    public GameObject panelCofre;
    public GameObject panelNota;

    public void MostrarCanvasSegunTipo(ObjectType tipo)
    {
        // Primero ocultamos todos para que no se traslapen
        DesactivarTodos();

        // Activamos solo el que necesitamos
        switch (tipo)
        {
            case ObjectType.Puerta:
                panelPuerta.SetActive(true);
                break;
            case ObjectType.Cofre:
                panelCofre.SetActive(true);
                break;
            case ObjectType.Nota:
                panelNota.SetActive(true);
                break;
        }
    }

    private void DesactivarTodos()
    {
        panelPuerta.SetActive(false);
        panelCofre.SetActive(false);
        panelNota.SetActive(false);
    }
}
