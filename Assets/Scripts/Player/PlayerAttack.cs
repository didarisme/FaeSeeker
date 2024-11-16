using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform playerLocal;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRadius, attackAngle;
    [SerializeField] private int damage = 1;

    private PlayerMove playerMove;
    private PlayerAnimation playerAnimation;

    private bool isAttacking = false;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            TryToAttack();
        }
    }

    private void TryToAttack()
    {
        if (isAttacking) return;

        if (playerAnimation != null)
            playerAnimation.PlayAttack();

        AttackDirection();
        StartCoroutine(PerformAttack());
    }

    private void AttackDirection()
    {
        Vector2 playerOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseDirection = (mousePosition - playerOnScreen).normalized;

        if (playerMove != null)
            playerMove.SetAttack(mouseDirection, true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRadius); // Центр и радиус
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        float timeElapsed = 0;

        while (timeElapsed < attackCooldown * 0.4f)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Collider[] hitColliders = Physics.OverlapSphere(playerLocal.position + Vector3.up, attackRadius);

        foreach (Collider collider in hitColliders)
        {
            Vector3 directionToTarget = (collider.transform.position - playerLocal.position).normalized;
            float angle = Vector3.Angle(playerLocal.forward, directionToTarget);

            if (angle < attackAngle / 2)
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.ApplyDamage(damage);
                    Debug.DrawRay(playerLocal.position + Vector3.up, directionToTarget, Color.yellow, attackRadius);
                }
            }
        }

        Debug.DrawRay(playerLocal.position, playerLocal.forward * attackRadius, Color.red, attackRadius);

        while (timeElapsed < attackCooldown)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
        playerMove.SetAttack(Vector2.zero, false);
    }
}