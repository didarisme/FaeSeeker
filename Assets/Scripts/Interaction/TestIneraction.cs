using System.Collections;
using UnityEngine;

public class TestIneraction : Interactable
{
    public override void OnFocus()
    {
        Debug.Log("Focus on " + gameObject.name);
    }

    public override void OnInteract()
    {
        Debug.Log("QWooowoqwow how did you do it?");
        Destroy(gameObject);
    }

    public override void OnLoseFocus()
    {
        Debug.Log("Lose focus from " + gameObject.name);
    }
}
