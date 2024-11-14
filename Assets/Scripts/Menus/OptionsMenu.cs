using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private List<EntryUIAnim> elementsUI;

    private MainMenuManager mainMenu;

    private void Awake()
    {
        mainMenu = GetComponent<MainMenuManager>();
    }

    public void OpenOptions()
    {
        foreach (EntryUIAnim element in elementsUI)
        {
            element.OnScreen(true);
        }
    }

    public void ExitOptions()
    {
        AudioManager.Instance.PlayAudio(AudioManager.SoundType.ButtonClick, 0, 0);

        foreach (EntryUIAnim element in elementsUI)
        {
            element.OnScreen(false);
        }

        mainMenu.ShowElements(true);
    }
}