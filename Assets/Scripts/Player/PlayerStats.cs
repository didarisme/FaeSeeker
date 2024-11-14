using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxMana = 100f;

    private float currentHealth, currentMana;

    public void OnTakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            KillPlayer();
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    public void OnManaUse(float manaAmount)
    {
        currentMana -= manaAmount;

        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
    }

    public void KillPlayer()
    {
        Debug.Log("Dead");
    }
}