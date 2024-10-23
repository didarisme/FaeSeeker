using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private List<EntryUIAnim> elementsUI;
    [SerializeField] private GameObject fadeIn;

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
        Debug.Log("Credits");
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
}