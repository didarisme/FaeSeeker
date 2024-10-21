using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private int sceneId = 1;

    public void PlayButton()
    {
        SceneManager.LoadScene(sceneId);
    }

    public void OptionsButton()
    {
        Debug.Log("Options");
    }

    public void CreditsButton()
    {
        Debug.Log("Credits");
    }

    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
