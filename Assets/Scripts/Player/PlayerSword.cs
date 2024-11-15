using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField]private Transform playerLocal;
    public float attackCooldown = 1f;
    public int attackDamage = 1;
    private bool isAttacking = false;
    PlayerMove playerMove;
    [SerializeField] PlayerAnimation playerAnimation;
    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(!isAttacking){
                isAttacking = true;
                MouseDirection();
                HitEnemies(GetEnemiesInRange());
                if(playerAnimation!=null){
                    playerAnimation.Attack();
                }
                StartCoroutine(AttackCooldown());
            }
            
        }
    }

    void MouseDirection(){
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseDirection = (mousePosition - screenCenter).normalized;
        if(playerMove!=null){
            playerMove.SetAttack(mouseDirection, true); 
        } 
          
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        playerMove.SetAttack(Vector2.zero, false);
    }

    //Spawns an Physics.OverlapBox that checks for Colliders that have EnemyHealth component
    List<EnemyHealth> GetEnemiesInRange(){
        
        Vector3 spawnPosition = new Vector3(playerLocal.localPosition.x, playerLocal.localPosition.y, playerLocal.localPosition.z + 1);
        spawnPosition = playerLocal.TransformPoint(spawnPosition);
        Vector3 halfExtents = new Vector3(1, 1, 1); // Adjust the size of the box as needed
        Collider[] overlappedColliders = Physics.OverlapBox(spawnPosition, halfExtents, Quaternion.identity);

        List<EnemyHealth> enemyList = new List<EnemyHealth>();
        foreach (Collider collider in overlappedColliders)
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemyList.Add(enemy);
            }
        }
        return enemyList;
    }

    void HitEnemies(List<EnemyHealth> enemies){
        foreach (EnemyHealth enemy in enemies)
        {
            enemy.ApplyDamage(attackDamage);
        }
    }

}
