using UnityEngine;

public class AttackState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.transform.position);
        enemy.EnemyAnimator.SetBool("isAttacking", true);
    }

    public override void Perform()
    {
        enemy.transform.LookAt(enemy.Player);

        float distance = Vector3.Distance(enemy.Player.position, enemy.transform.position);

        if (distance > 2f)
            stateMachine.ChangeState(new ChaseState());
    }

    public override void Exit()
    {
        enemy.EnemyAnimator.SetBool("isAttacking", false);
    }
}
