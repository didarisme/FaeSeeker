using UnityEngine;

public class AttackState : BaseState
{
    public override void Enter()
    {
        npc.Agent.SetDestination(npc.Player.position);
        npc.Agent.speed = 0.1f;
        npc.CharacterAnimator.SetBool("isAttacking", true);
    }

    public override void Perform()
    {
        npc.transform.LookAt(npc.Player);

        float distance = Vector3.Distance(npc.Player.position, npc.transform.position);

        if (distance < 0.4)
            npc.Agent.speed = 0;
        else
            npc.Agent.speed = 0.1f;

        if (distance > 2.1f)
            stateMachine.ChangeState(new ChaseState());
    }

    public override void Exit()
    {
        npc.CharacterAnimator.SetBool("isAttacking", false);
    }
}
