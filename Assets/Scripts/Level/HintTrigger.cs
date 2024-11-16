using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] private string hintText;
    [SerializeField] private Transform newCameraTarget;

    private void OnTriggerEnter(Collider other)
    {
        HintPanel.OnHint?.Invoke(hintText, newCameraTarget);
        GetComponent<Collider>().enabled = false;
    }
}
