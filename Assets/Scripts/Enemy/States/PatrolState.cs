using UnityEngine;

public class PatrolState : BaseState
{
    private float timeElapsed;
    private float patrolTimer;
    private bool isRandomPath;
    private Vector3 patrolPoint;

    public override void Enter()
    {
        npc.CharacterAnimator.SetBool("isPatrolling", true);
        npc.Agent.speed = npc.parameters.movement.patrolSpeed;

        Vector2 timeRange = npc.parameters.timeRanges.patrolTimer;
        patrolTimer = Random.Range(timeRange.x, timeRange.y);

        if (npc.path == null)
        {
            isRandomPath = true;

            if (npc.lastPatrolPoint != Vector3.zero)
            {
                npc.Agent.SetDestination(npc.lastPatrolPoint);
            }
            else
            {
                patrolPoint = npc.RandomDestination();
                npc.Agent.SetDestination(patrolPoint);
                Debug.Log("Check calculations! " + patrolPoint + npc.Agent.destination);
            }
        }
        else
        {
            npc.Agent.SetDestination(npc.path.wayPoints[npc.waypointIndex].position);
        }
    }

    public override void Perform()
    {
        PatrolCycle();

        if (npc.CanSeePlayer() || npc.CanHearPlayer())
        {
            stateMachine.ChangeState(new ChaseState());
        }
    }

    public override void Exit()
    {
        npc.CharacterAnimator.SetBool("isPatrolling", false);
    }

    private void PatrolCycle()
    {
        timeElapsed += Time.deltaTime;

        if (isRandomPath && npc.Agent.remainingDistance < 0.1f)
        {
            patrolPoint = npc.RandomDestination();
            npc.Agent.SetDestination(patrolPoint);
            Debug.Log("Check calculations! " + patrolPoint + npc.Agent.destination);

            npc.lastPatrolPoint = npc.Agent.destination;

            CheckForIdle();
        }
        else if (!isRandomPath && npc.Agent.remainingDistance < 0.1f)
        {
            if (npc.waypointIndex < npc.path.wayPoints.Count - 1)
                npc.waypointIndex++;
            else
                npc.waypointIndex = 0;

            npc.Agent.SetDestination(npc.path.wayPoints[npc.waypointIndex].position);

            CheckForIdle();
        }

        if (timeElapsed > patrolTimer)
        {
            stateMachine.ChangeState(new IdleState());
        }
    }

    private void CheckForIdle()
    {
        float idleChance = Random.value;
        Debug.Log("Chance for idle: " + idleChance);

        if (idleChance <= 0.5f)
            stateMachine.ChangeState(new IdleState());
    }
}