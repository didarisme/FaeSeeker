using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    public string sceneChoice;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add your scene transition logic here
            StartCoroutine(DelaySceneTransition(1.5f,sceneChoice));
        }
    }

    private IEnumerator DelaySceneTransition(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        ChangeScene(sceneName);
    }
    
    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    
}
