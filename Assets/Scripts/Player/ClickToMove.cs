using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] LayerMask clickableLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, clickableLayer))
            {
                agent.SetDestination(hit.point);
            }
        }

    }
}
