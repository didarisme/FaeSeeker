using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class SceneInterface : MonoBehaviour
{
    public string nextScene = "SampleScene";
    public TMP_InputField sceneInput;
    public void Quit(){
        Application.Quit();
    }
    public void LoadNextScene(){
        try
        {
            SceneManager.LoadScene(nextScene);
        }
        catch (System.Exception e)
        {
            Debug.LogError(this.name + ": Failed to load scene: " + e.Message);
        }
    }
    public void AcceptInput(){
        nextScene = sceneInput.text;
        LoadNextScene();
    }
}
