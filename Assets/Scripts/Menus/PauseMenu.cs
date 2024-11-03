using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private GameObject fadeIn;

    private EntryUIAnim entryAnim;

    private void Awake()
    {
        entryAnim = GetComponent<EntryUIAnim>();
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void ContinueBtn()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ExitLevelBtn()
    {
        fadeIn.SetActive(true);
    }
}