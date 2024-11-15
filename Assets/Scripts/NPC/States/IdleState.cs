using UnityEngine;

public class IdleState : BaseState
{
    private float timeElapsed;
    private float idleTimer;

    public override void Enter()
    {
        npc.Agent.SetDestination(npc.transform.position);
        Vector2 timeRange = npc.parameters.timeRanges.idleTimer;
        idleTimer = Random.Range(timeRange.x, timeRange.y);
    }

    public override void Perform()
    {
        Idling();

        if (npc.CanSeePlayer() || npc.CanHearPlayer())
        {
            stateMachine.ChangeState(new ChaseState());
        }
    }

    public override void Exit()
    {

    }

    private void Idling()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= idleTimer)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }
}