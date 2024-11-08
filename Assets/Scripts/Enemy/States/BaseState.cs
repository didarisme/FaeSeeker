public abstract class BaseState
{
    public NonPlayerCharacter npc;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}