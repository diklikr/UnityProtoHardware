using UnityEngine;
public class IdleState : State
{
    public IdleState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void LogicUpdate()
    {
        // Si el agente empieza a moverse, cambiamos a Walking
        if (player.agent.velocity.magnitude > 0.1f)
        {
            stateMachine.ChangeState(player.walkingState);
        }
    }

    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                player.agent.SetDestination(hit.point);
                stateMachine.ChangeState(player.walkingState);
            }
        }
    }
}