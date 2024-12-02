using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 12;
    [SerializeField] private int maxMana = 6;

    private int currentHealth, currentMana;

    private PlayerStatsUI playerStatsUI;
    private GameManager gameManager;

    private void Start()
    {
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public void OnHealth(int increaseAmount)
    {
        currentHealth += increaseAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            KillPlayer();
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (playerStatsUI != null)
            playerStatsUI.UpdateHealthUI(currentHealth);
    }

    public bool OnMana(int increaseAmount)
    {
        currentMana += increaseAmount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        if (playerStatsUI != null)
            playerStatsUI.UpdateManaUI(currentMana);

        return currentMana > 0;
    }

    public void KillPlayer()
    {
        if (gameManager != null)
            gameManager.EndGame();
    }
}