using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int sceneInd = 1;

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneInd);
    }
}