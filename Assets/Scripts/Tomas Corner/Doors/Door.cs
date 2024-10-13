using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool key = true;
    public float openSpeed = 2.0f; // Speed of opening
    public float closeSpeed = 2.0f; // Speed of closing
    public Transform openPosition; // Position when the door is open
    public Transform closedPosition; // Position when the door is closed

    private Coroutine currentCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(Open());
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(Close());
        }
    }

    private IEnumerator Open()
    {
        Vector3 targetPosition = openPosition.position;
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.localPosition = targetPosition;
    }

    private IEnumerator Close()
    {
        Vector3 targetPosition = closedPosition.position;
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * closeSpeed);
            yield return null;
        }
        transform.localPosition = targetPosition;
    }

}
