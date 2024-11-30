using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private Image fadeOut;

    [Header("Controls")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private MethodInvoker methodInvoker;
    private SceneChanger sceneChanger;

    private void Awake()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.numerator;
        methodInvoker = fadeIn.GetComponent<MethodInvoker>();
        sceneChanger = GetComponentInChildren<SceneChanger>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !fadeOut.enabled)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0;

        methodInvoker.MethodToInvoke.RemoveAllListeners();
        methodInvoker.MethodToInvoke.AddListener(sceneChanger.ReloadScene);
        fadeIn.SetActive(true);
    }
}