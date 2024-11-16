using UnityEngine;
using UnityEngine.Events;

public class MethodInvoker : MonoBehaviour
{
    public UnityEvent MethodToInvoke;

    public void InvokeThisMethod()
    {
        MethodToInvoke?.Invoke();
    }
}