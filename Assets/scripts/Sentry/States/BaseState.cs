public abstract class BaseState
{
    public StateMachine stateMachine;
    public Sentry sentry;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}