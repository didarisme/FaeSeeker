using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Controls")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            pauseMenu.SetActive(true);
        }
    }
}