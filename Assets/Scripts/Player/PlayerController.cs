using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public StateMachine stateMachine;

    // Instancias de los estados
    public IdleState idleState;
    public WalkingState walkingState;

    void Start()
    {
        stateMachine = new StateMachine();

        // Inicializamos los estados
        idleState = new IdleState(this, stateMachine);
        walkingState = new WalkingState(this, stateMachine);

        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        // Esto es vital: le dice al estado actual que ejecute su lógica
        stateMachine.CurrentState.HandleInput();
        stateMachine.CurrentState.LogicUpdate();

        // El Animator se queda aquí porque es global
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }
}