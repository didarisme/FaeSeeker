using UnityEngine;

public class AttackState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.Player.position);
        enemy.Agent.speed = 0.1f;
        enemy.EnemyAnimator.SetBool("isAttacking", true);
    }

    public override void Perform()
    {
        enemy.transform.LookAt(enemy.Player);

        float distance = Vector3.Distance(enemy.Player.position, enemy.transform.position);

        if (distance < 0.4)
            enemy.Agent.speed = 0;
        else
            enemy.Agent.speed = 0.1f;

        if (distance > 2.1f)
            stateMachine.ChangeState(new ChaseState());
    }

    public override void Exit()
    {
        enemy.EnemyAnimator.SetBool("isAttacking", false);
    }
}
