using UnityEngine;

public class WalkingState : State
{
    public WalkingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        // Aquí podrías disparar un trigger de animación si fuera necesario
        Debug.Log("Entrando al estado: WALKING");

        player.animator.SetTrigger("StartWalking");
    }

    public override void HandleInput()
    {
        // Permitimos que el jugador cambie de destino mientras camina (clic de nuevo)
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
        // Si el agente se detiene o llega a su destino, volvemos a Idle
        // Usamos pathPending para asegurarnos de que el NavMesh ya calculó la ruta
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