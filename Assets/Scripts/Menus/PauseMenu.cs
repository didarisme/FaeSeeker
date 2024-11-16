using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;

    private PlayerAttack playerAttack;
    private Animator pauseAnimator;

    private void Awake()
    {
        pauseAnimator = GetComponentInChildren<Animator>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    private void OnEnable()
    {
        playerAttack.enabled = false;
        Time.timeScale = 0;
    }

    public void NoBtn()
    {
        pauseAnimator.SetTrigger("OnPauseEnd");
    }

    public void YesBtn()
    {
        fadeIn.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        playerAttack.enabled = true;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}