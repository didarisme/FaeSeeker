using System.Collections;
using UnityEngine;

public class SleepGates : MonoBehaviour
{
    [SerializeField] private PressurePlate plate;
    [SerializeField] private Transform sleepGate;
    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private float timeToComplete = 1.5f;

    private void OnEnable()
    {
        if (plate != null)
            plate.OnPlatePressure += OpenGate;
    }

    private void OnDisable()
    {
        if (plate != null)
            plate.OnPlatePressure -= OpenGate;
    }

    private void Start()
    {
        if (sleepGate != null)
            closedPosition = sleepGate.localPosition;
        else
            Debug.Log(sleepGate + " why?");
    }

    private void OpenGate(bool isOpen)
    {
        if (isOpen)
        {
            StopAllCoroutines();

            StartCoroutine(MoveGate(isOpen, openPosition));
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(MoveGate(isOpen, closedPosition));
        }
    }

    private IEnumerator MoveGate(bool isOpen, Vector3 targetPos)
    {
        Vector3 startPos = sleepGate.localPosition;
        float timeElapsed = 0;

        while (timeElapsed <= timeToComplete)
        {
            sleepGate.localPosition = Vector3.Lerp(startPos, targetPos, timeElapsed / timeToComplete);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}