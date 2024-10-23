using UnityEngine;
using UnityEngine.Events;

public class MethodInvoker : MonoBehaviour
{
    [SerializeField] private UnityEvent methodToInvoke;

    public void InvokeThisMethod()
    {
        methodToInvoke.Invoke();
    }
}