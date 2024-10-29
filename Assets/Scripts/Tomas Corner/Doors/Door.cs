using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool state = true;
    public float moveTime = 2f;
    public Transform openTransform; // Position when the door is open
    public Transform closedTransform; // Position when the door is closed
    private Coroutine currentCoroutine;

    bool ready = true;
    
    private void Update(){
        if(Input.GetKeyDown(KeyCode.O)){
            Open();
        }
        if(Input.GetKeyDown(KeyCode.C)){
            Close();
        }
    }
    public void Open(){
        if(!ready){
            StopAllCoroutines();
        }
        currentCoroutine = StartCoroutine(Move(openTransform.position));
    }

    public void Close(){
        if(!ready){
            StopAllCoroutines();
        }
        currentCoroutine = StartCoroutine(Move(closedTransform.position));
    }
    private IEnumerator Move(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        ready = false;
        float timeElapsed = 0f;
        while (timeElapsed < moveTime)
        {
            timeElapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed/moveTime);
            yield return null;
        }

        ready = true;
    }

}
