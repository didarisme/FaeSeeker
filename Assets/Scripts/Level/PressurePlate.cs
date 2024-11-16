using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public event Action<bool> OnPlatePressure;

    private int counter;

    private void OnTriggerEnter(Collider other)
    {
        counter++;

        if (counter == 1)
            OnPlatePressure?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        counter--;

        if (counter == 0)
            OnPlatePressure?.Invoke(false);
    }
}