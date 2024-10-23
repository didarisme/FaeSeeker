using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<EntryUIAnim> elementsUI;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private GameObject credits;

    private OptionsMenu optionsMenu;

    private void Start()
    {
        optionsMenu = GetComponent<OptionsMenu>();
        ShowElements(true);
    }

    public void PlayButton()
    {
        ShowElements(false);
        fadeIn.SetActive(true);
    }

    public void OptionsButton()
    {
        ShowElements(false);
        optionsMenu.OpenOptions();
    }

    public void CreditsButton()
    {
        ShowElements(false);
        credits.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ShowElements(bool boolState)
    {
        foreach (EntryUIAnim element in elementsUI)
        {
            element.OnScreen(boolState);
        }
    }

    public void EndCreditsComplete()
    {
        credits.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}