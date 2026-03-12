public abstract class State
{
    protected PlayerController player;
    protected StateMachine stateMachine;

    // Constructor para que el estado sepa quién es el player
    protected State(PlayerController player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { } // Se ejecuta al entrar al estado
    public virtual void HandleInput() { } // Detectar clics
    public virtual void LogicUpdate() { } // Update normal
    public virtual void Exit() { } // Se ejecuta al salir
}