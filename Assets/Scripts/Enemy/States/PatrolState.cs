using UnityEngine;

public class PatrolState : BaseState
{
    private float timeElapsed;
    private float patrolTimer;
    private bool isRandomPath;
    private Vector3 patrolPoint;

    public override void Enter()
    {
        if (npc.isAnimatorExist)
            npc.CharacterAnimator.SetBool("isPatrolling", true);

        npc.Agent.speed = npc.parameters.movement.patrolSpeed;

        Vector2 timeRange = npc.parameters.timeRanges.patrolTimer;
        patrolTimer = Random.Range(timeRange.x, timeRange.y);

        if (npc.path == null)
        {
            isRandomPath = true;

            if (npc.LastPatrolPoint != Vector3.zero)
            {
                npc.Agent.SetDestination(npc.LastPatrolPoint);
            }
            else
            {
                patrolPoint = npc.RandomDestination();
                npc.Agent.SetDestination(patrolPoint);
                //Debug.Log("Check calculations! " + patrolPoint + npc.Agent.destination);
            }
        }
        else
        {
            npc.Agent.SetDestination(npc.path.wayPoints[npc.WaypointIndex].position);
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
        if (npc.isAnimatorExist)
            npc.CharacterAnimator.SetBool("isPatrolling", false);
    }

    private void PatrolCycle()
    {
        timeElapsed += Time.deltaTime;

        if (isRandomPath && npc.Agent.remainingDistance < 0.1f)
        {
            patrolPoint = npc.RandomDestination();
            npc.Agent.SetDestination(patrolPoint);
            //Debug.Log("Check calculations! " + patrolPoint + npc.Agent.destination);

            npc.LastPatrolPoint = npc.Agent.destination;

            CheckForIdle(0.5f);
        }
        else if (!isRandomPath && npc.Agent.remainingDistance < 0.1f)
        {
            if (npc.WaypointIndex < npc.path.wayPoints.Count - 1)
                npc.WaypointIndex++;
            else
                npc.WaypointIndex = 0;

            npc.Agent.SetDestination(npc.path.wayPoints[npc.WaypointIndex].position);

            CheckForIdle(-1f);
        }

        if (timeElapsed > patrolTimer)
        {
            stateMachine.ChangeState(new IdleState());
        }
    }

    private void CheckForIdle(float chance)
    {
        float idleChance = Random.value;
        //Debug.Log("Chance for idle: " + idleChance);

        if (idleChance <= chance)
            stateMachine.ChangeState(new IdleState());
    }
}