using UnityEngine;

public class IdleState : BaseState
{
    private float timeElapsed;
    private float idleTimer;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.transform.position);
        Vector2 timeRange = enemy.npc.timeRanges.idleTimer;
        idleTimer = Random.Range(timeRange.x, timeRange.y);
    }

    public override void Perform()
    {
        Idling();

        if (enemy.CanSeePlayer() || enemy.CanHearPlayer())
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