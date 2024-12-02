using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;

    private DaxForms daxForms;
    private Animator pauseAnimator;

    private void Awake()
    {
        pauseAnimator = GetComponentInChildren<Animator>();
        daxForms = FindObjectOfType<DaxForms>();
    }

    private void OnEnable()
    {
        daxForms.enabled = false;
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
        daxForms.enabled = true;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}