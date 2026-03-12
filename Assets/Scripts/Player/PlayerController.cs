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
        // La máquina de estados controla la lógica
        stateMachine.CurrentState.LogicUpdate();
        stateMachine.CurrentState.HandleInput();

        // La animación sigue dependiendo de la velocidad
        animator.SetFloat("Speed", agent.velocity.magnitude, 0.1f, Time.deltaTime);
    }
}