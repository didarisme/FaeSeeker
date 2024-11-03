using System.Collections;
using UnityEngine;

public class SleepGates : MonoBehaviour
{
    [SerializeField] private PressurePlate plate;
    [SerializeField] private Transform sleepGate;
    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private float gateSpeed = 1.5f;

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

            StartCoroutine(MoveGate(openPosition));
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(MoveGate(closedPosition));
        }
    }

    private IEnumerator MoveGate(Vector3 targetPos)
    {
        while (Vector3.Distance(sleepGate.localPosition, targetPos) > 0.01f)
        {
            sleepGate.localPosition = Vector3.MoveTowards(sleepGate.localPosition, targetPos, gateSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        sleepGate.localPosition = targetPos;
    }
}