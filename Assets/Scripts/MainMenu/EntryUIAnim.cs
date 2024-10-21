using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryUIAnim : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float targetRotation;
    [SerializeField] private float entryTime = 0.7f;
    private float timeElapsed = 0f;

    private RectTransform rectTransform;

    private bool isMoving = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    private void MoveToTarget()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, timeElapsed / entryTime);
        rectTransform.localRotation = Quaternion.Euler(rectTransform.localRotation.x, rectTransform.localRotation.y, Mathf.Lerp(rectTransform.localRotation.z, targetRotation, timeElapsed / entryTime));

        if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 0.01f)
        {
            rectTransform.anchoredPosition = targetPosition;
            isMoving = false;
        }
    }
}