using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttons : MonoBehaviour
{
    public string nextScene = "SampleScene";
    public TMP_InputField sceneInput;
    public void Quit(){
        Application.Quit();
    }
    public void LoadNextScene(){
        SceneManager.LoadScene(nextScene);
    }
    public void AcceptInput(){

        nextScene = sceneInput.text;
        LoadNextScene();
    }
}
