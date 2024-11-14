using UnityEngine;

public class ChaseState : BaseState
{
    public override void Enter()
    {
        npc.Agent.SetDestination(npc.Player.position);
        npc.Agent.speed = npc.parameters.movement.chaseSpeed;
        npc.CharacterAnimator.SetBool("isChasing", true);
    }

    public override void Perform()
    {
        if (npc.CanSeePlayer() || npc.CanHearPlayer())
        {
            npc.Agent.SetDestination(npc.Player.position);

            if (npc.PlayerDistance < 1f)
                stateMachine.ChangeState(new AttackState());
        }
        else
        {
            npc.LastKnowPos = npc.Player.position;
            stateMachine.ChangeState(new SearchState());
            npc.CharacterAnimator.SetBool("isChasing", false);
        }
    }

    public override void Exit()
    {
        npc.CharacterAnimator.SetBool("isChasing", false);
    }
}