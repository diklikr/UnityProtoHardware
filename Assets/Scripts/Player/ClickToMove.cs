using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask clickableLayer;

    private void Update()
    {
        // 1. Primero detectamos si el jugador hizo clic
        if (Input.GetMouseButtonDown(0))
        {
            // 2. En el instante del clic, preguntamos: ¿El mouse está sobre una interfaz (UI)?
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // Si la respuesta es sí, imprimimos esto y CANCELAMOS todo lo demás
                Debug.Log("🛡️ CLIC BLOQUEADO: El mouse tocó la UI. El NPC no debería moverse.");
                return;
            }

            // 3. Si el código llega hasta aquí, significa que el clic fue en el piso 3D
            Debug.Log("🚶 CLIC LIBRE: Movimiento 3D detectado.");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickableLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
