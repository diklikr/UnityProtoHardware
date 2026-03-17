using UnityEngine;
using UnityEngine.EventSystems; // Librería necesaria

public class WalkingState : State
{
    public WalkingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entrando al estado: WALKING");
        player.animator.SetTrigger("StartWalking");
    }

    public override void HandleInput()
    {
        // --- ESCUDO ANTI-UI ---
        //if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        //{
        //    return; // Si tocamos un botón, evitamos recalcular la ruta
        //}

        // Permitimos que el jugador cambie de destino mientras camina
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                player.agent.SetDestination(hit.point);
            }
        }
    }

    public override void LogicUpdate()
    {
        if (!player.agent.pathPending && player.agent.remainingDistance <= player.agent.stoppingDistance)
        {
            if (!player.agent.hasPath || player.agent.velocity.sqrMagnitude == 0f)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Saliendo del estado: WALKING");
    }
}