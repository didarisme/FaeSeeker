using UnityEngine;

public class PatrolState : BaseState
{
    private float timeElapsed;
    private float patrolTimer;

    public override void Enter()
    {
        enemy.EnemyAnimator.SetBool("isPatrolling", true);
        enemy.Agent.speed = enemy.npc.movement.patrolSpeed;
        Vector2 timeRange = enemy.npc.timeRanges.patrolTimer;
        patrolTimer = Random.Range(timeRange.x, timeRange.y);
        enemy.Agent.SetDestination(enemy.path.wayPoints[enemy.waypointIndex].position);
    }

    public override void Perform()
    {
        PatrolCycle();

        if (enemy.CanSeePlayer() || enemy.CanHearPlayer())
        {
            stateMachine.ChangeState(new ChaseState());
        }
    }

    public override void Exit()
    {
        enemy.EnemyAnimator.SetBool("isPatrolling", false);
    }

    public void PatrolCycle()
    {
        timeElapsed += Time.deltaTime;

        if (enemy.Agent.remainingDistance < 0.1f)
        {
            if (enemy.waypointIndex < enemy.path.wayPoints.Count - 1)
                enemy.waypointIndex++;
            else
                enemy.waypointIndex = 0;

            enemy.Agent.SetDestination(enemy.path.wayPoints[enemy.waypointIndex].position);
            stateMachine.ChangeState(new IdleState());
        }
        else if (timeElapsed > patrolTimer)
        {
            stateMachine.ChangeState(new IdleState());
        }
    }
}