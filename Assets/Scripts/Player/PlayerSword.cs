using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField]private Transform playerLocal;
    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    PlayerMove playerMove;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            MouseDirection();
            HitEnemies(GetEnemiesInRange());
        }
    }

    void MouseDirection(){
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mousePosition = Input.mousePosition;

        Vector2 mouseDirection = (mousePosition - screenCenter).normalized; 
        //playerMove.OnAttack(true,mouseDirection);
        StartCoroutine(ResetAttack(mouseDirection));
    }

    IEnumerator ResetAttack(Vector2 mouseDirection)
    {
        yield return new WaitForSeconds(1.0f);
        //playerMove.OnAttack(false, mouseDirection);
    }

    List<NonPlayerCharacter> GetEnemiesInRange(){
        
        Vector3 spawnPosition = new Vector3(playerLocal.localPosition.x, playerLocal.localPosition.y, playerLocal.localPosition.z + 1);
        spawnPosition = playerLocal.TransformPoint(spawnPosition);
        Vector3 halfExtents = new Vector3(1, 1, 1); // Adjust the size of the box as needed
        Collider[] overlappedColliders = Physics.OverlapBox(spawnPosition, halfExtents, Quaternion.identity);

        List<NonPlayerCharacter> enemyList = new List<NonPlayerCharacter>();
        foreach (Collider collider in overlappedColliders)
        {
            NonPlayerCharacter npc = collider.GetComponent<NonPlayerCharacter>();
            if (npc != null)
            {
                enemyList.Add(npc);
            }
        }
        return enemyList;
        //SpawnCube(spawnPosition);
        
    }

    void HitEnemies(List<NonPlayerCharacter> enemies){
        foreach (NonPlayerCharacter enemy in enemies)
        {
            //defeat NPC
            Destroy(enemy);
            
        }
    }

}
