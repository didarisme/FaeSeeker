using UnityEngine;

public class SleepObjects : MonoBehaviour
{
    [SerializeField] private PressurePlate plate;
    [SerializeField] private GameObject[] sleepObjects;

    private void OnEnable()
    {
        if (plate != null)
            plate.OnPlatePressure += WakeHim;
    }

    private void OnDisable()
    {
        if (plate != null)
            plate.OnPlatePressure -= WakeHim;
    }

    private void WakeHim(bool newBool)
    {
        foreach (GameObject sleepObject in sleepObjects)
        {
            sleepObject.SetActive(newBool);
        }
    }
}