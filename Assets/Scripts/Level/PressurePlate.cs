using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public event Action<bool> OnPlatePressure;

    private int counter;

    private void OnTriggerEnter(Collider other)
    {
        OnPlatePressure?.Invoke(true);
        counter++;
    }

    private void OnTriggerExit(Collider other)
    {
        counter--;

        if (counter <= 0)
        {
            OnPlatePressure?.Invoke(false);
        }
    }
}