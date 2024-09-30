using UnityEngine;

public class IdleState : BaseState
{
    private float timeElapsed;
    private float waitTimer;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.transform.position);
        waitTimer = Random.Range(1.5f, 6f);
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

        if (timeElapsed >= waitTimer)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }
}