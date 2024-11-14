using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Image[] healthImages;
    [SerializeField] private Image[] manaImages;

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            healthImages[i].enabled = i < currentHealth;
        }
    }

    public void UpdateManaUI(int currentMana)
    {
        for (int i = 0; i < manaImages.Length; i++)
        {
            manaImages[i].enabled = i < currentMana;
        }
    }
}
