using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int sceneInd)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneInd);
    }
}