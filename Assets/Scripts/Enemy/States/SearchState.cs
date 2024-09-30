using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimeElapsed;
    private float moveTimeElapsed;
    private float searchTimer;
    private float moveTimer;

    public override void Enter()
    {
        enemy.EnemyAnimator.SetBool("isPatrolling", true);
        enemy.Agent.SetDestination(enemy.LastKnowPos);
        enemy.Agent.speed = enemy.patrolSpeed;
        moveTimer = Random.Range(1, 5);
        searchTimer = Random.Range(20, 45);
    }

    public override void Perform()
    {
        searchTimeElapsed += Time.deltaTime;

        if (enemy.CanSeePlayer() || enemy.CanHearPlayer())
            stateMachine.ChangeState(new ChaseState());

        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            moveTimeElapsed += Time.deltaTime;

            enemy.EnemyAnimator.SetBool("isPatrolling", false);

            if (moveTimeElapsed > moveTimer)
            {
                enemy.EnemyAnimator.SetBool("isPatrolling", true);
                Debug.Log("new point");
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10f));
                moveTimer = Random.Range(1, 5);
                moveTimeElapsed = 0;
            }
        }
        if (searchTimeElapsed > searchTimer)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }

    public override void Exit()
    {
        enemy.EnemyAnimator.SetBool("isPatrolling", false);
    }
}