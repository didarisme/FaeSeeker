using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private GameObject dustPrefab;

    private NonPlayerCharacter npc;

    private void Start()
    {
        npc = GetComponent<NonPlayerCharacter>();
        enemyHealth = npc.parameters.behaviour.health;
    }

    public void ApplyDamage(int damageValue)
    {
        enemyHealth -= damageValue;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            Kill();
        }

        Debug.Log(gameObject.name + " health: " + enemyHealth + " / Current damage: " + damageValue);
    }

    private void Kill()
    {
        Instantiate(dustPrefab, transform.position + (Vector3.up / 2), transform.rotation);
        Destroy(gameObject);
    }
}