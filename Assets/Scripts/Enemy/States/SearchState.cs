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
        npc.CharacterAnimator.SetBool("isPatrolling", true);
        npc.Agent.SetDestination(npc.LastKnowPos);
        npc.Agent.speed = npc.parameters.movement.patrolSpeed;

        idleTimeRange = npc.parameters.timeRanges.searchIdleTimer;
        Vector2 searchTimeRange = npc.parameters.timeRanges.searchTimer;
        idleTimer = Random.Range(idleTimeRange.x, idleTimeRange.y);
        searchTimer = Random.Range(searchTimeRange.x, searchTimeRange.y);
        //Debug.Log("Search State Started");
        //Debug.Log("move time: " + idleTimer);
        //Debug.Log("search time: " + searchTimer);
    }

    public override void Perform()
    {
        searchTimeElapsed += Time.deltaTime;

        if (npc.CanSeePlayer() || npc.CanHearPlayer())
            stateMachine.ChangeState(new ChaseState());

        if (npc.Agent.remainingDistance < npc.Agent.stoppingDistance)
        {
            moveTimeElapsed += Time.deltaTime;

            npc.CharacterAnimator.SetBool("isPatrolling", false);

            if (moveTimeElapsed > idleTimer)
            {
                Vector3 randomPoint = npc.RandomDestination();
                npc.Agent.SetDestination(randomPoint);

                //Debug.Log("New Point " + npc.Agent.destination);

                npc.CharacterAnimator.SetBool("isPatrolling", true);

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
        //Debug.Log("Exit!");
        npc.CharacterAnimator.SetBool("isPatrolling", false);
    }
}