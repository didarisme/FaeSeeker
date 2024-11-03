using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ContinueBtn()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitLevelBtn()
    {
        fadeIn.SetActive(true);
    }
}