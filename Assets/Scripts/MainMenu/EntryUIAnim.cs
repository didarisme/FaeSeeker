using System.Collections;
using UnityEngine;

public class EntryUIAnim : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float targetRotation;
    [SerializeField] private float entryTime = 0.7f;

    private Vector2 defaultPosition;
    private float defaultRotation;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPosition = rectTransform.anchoredPosition;
        defaultRotation = rectTransform.eulerAngles.z;
    }

    public void OnScreen(bool isOnScreen)
    {
        if (isOnScreen)
            StartCoroutine(MoveToTarget(defaultPosition, defaultRotation, targetPosition, targetRotation));
        else
            StartCoroutine(MoveToTarget(targetPosition, targetRotation, defaultPosition, defaultRotation));
    }

    private IEnumerator MoveToTarget(Vector2 startPos, float startRot, Vector3 targetPos, float targetRot)
    {
        float timeElapsed = 0f;

        while (timeElapsed < entryTime)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, timeElapsed / entryTime);
            rectTransform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(startRot, targetRot, timeElapsed / entryTime)); ;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        rectTransform.localRotation = Quaternion.Euler(0, 0, targetRot);
    }
}