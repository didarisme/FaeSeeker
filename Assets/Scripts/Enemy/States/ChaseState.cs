using UnityEngine;

public class ChaseState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.Player.position);
        enemy.Agent.speed = enemy.npc.movement.chaseSpeed;
        enemy.EnemyAnimator.SetBool("isChasing", true);
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer() || enemy.CanHearPlayer())
        {
            enemy.Agent.SetDestination(enemy.Player.position);

            float distance = Vector3.Distance(enemy.Player.position, enemy.transform.position);

            if (distance < 2f)
                stateMachine.ChangeState(new AttackState());
        }
        else
        {
            enemy.LastKnowPos = enemy.Player.position;
            stateMachine.ChangeState(new SearchState());
            enemy.EnemyAnimator.SetBool("isChasing", false);
        }
    }

    public override void Exit()
    {
        //enemy.EnemyAnimator.SetBool("isChasing", false);
    }
}