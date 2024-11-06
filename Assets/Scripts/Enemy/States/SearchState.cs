using UnityEngine;

public class SearchState : BaseState
{
    private Vector2 idleTimeRange;

    private float searchTimeElapsed;
    private float moveTimeElapsed;
    private float searchTimer;
    private float idleTimer;

    public override void Enter()
    {
        enemy.EnemyAnimator.SetBool("isPatrolling", true);
        enemy.Agent.SetDestination(enemy.LastKnowPos);
        enemy.Agent.speed = enemy.npc.movement.patrolSpeed;

        idleTimeRange = enemy.npc.timeRanges.searchMoveTimer;
        Vector2 searchTimeRange = enemy.npc.timeRanges.searchTimer;
        idleTimer = Random.Range(idleTimeRange.x, idleTimeRange.y);
        searchTimer = Random.Range(searchTimeRange.x, searchTimeRange.y);
        Debug.Log("Search State Started");
        Debug.Log("move time: " + idleTimer);
        Debug.Log("search time: " + searchTimer);
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

            if (moveTimeElapsed > idleTimer)
            {
                Vector3 previousDestionation = enemy.Agent.destination;
                Vector3 randomPosition = Random.insideUnitSphere * 10f;
                Vector3 newDestination = enemy.transform.position + randomPosition;

                enemy.Agent.SetDestination(newDestination);

                while (previousDestionation == enemy.Agent.destination)
                {
                    Debug.Log("Recalculating");
                    randomPosition = Random.insideUnitSphere * 10f;
                    newDestination = enemy.transform.position + randomPosition;
                    enemy.Agent.SetDestination(newDestination);
                }

                Debug.Log("New Point " + enemy.Agent.destination);

                enemy.EnemyAnimator.SetBool("isPatrolling", true);

                idleTimer = Random.Range(idleTimeRange.x, idleTimeRange.y);
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
        Debug.Log("Exit!");
        enemy.EnemyAnimator.SetBool("isPatrolling", false);
    }
}