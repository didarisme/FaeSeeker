using UnityEngine;

public class ChaseState : BaseState
{
    public override void Enter()
    {
        npc.Agent.SetDestination(npc.Player.position);
        npc.Agent.speed = npc.parameters.movement.chaseSpeed;
        npc.EnemyAnimator.SetBool("isChasing", true);
    }

    public override void Perform()
    {
        if (npc.CanSeePlayer() || npc.CanHearPlayer())
        {
            npc.Agent.SetDestination(npc.Player.position);

            float distance = Vector3.Distance(npc.Player.position, npc.transform.position);

            if (distance < 2f)
                stateMachine.ChangeState(new AttackState());
        }
        else
        {
            npc.LastKnowPos = npc.Player.position;
            stateMachine.ChangeState(new SearchState());
            npc.EnemyAnimator.SetBool("isChasing", false);
        }
    }

    public override void Exit()
    {
        //enemy.EnemyAnimator.SetBool("isChasing", false);
    }
}