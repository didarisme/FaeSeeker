using System.Collections;
using UnityEngine;

public class SleepGates : MonoBehaviour
{
    [SerializeField] private PressurePlate[] plates;
    [SerializeField] private Transform sleepGate;
    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private float gateSpeed = 1.5f;

    private int activePlates;

    private void OnEnable()
    {
        foreach (var plate in plates)
        {
            plate.OnPlatePressure += HandlePlatePressure;
        }
    }

    private void OnDisable()
    {
        foreach (var plate in plates)
        {
            plate.OnPlatePressure -= HandlePlatePressure;
        }
    }

    private void Start()
    {
        if (sleepGate != null)
            closedPosition = sleepGate.localPosition;
        else
            Debug.LogError(sleepGate + " is not assigned properly.");
    }

    private void HandlePlatePressure(bool isPressed)
    {
        if (isPressed)
        {
            activePlates++;
        }
        else
        {
            activePlates--;
        }

        // Проверяем, все ли плиты активированы
        bool shouldOpenGate = activePlates == plates.Length;

        if (shouldOpenGate)
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
            yield return null;
        }

        sleepGate.localPosition = targetPos;
    }
}