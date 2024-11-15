using UnityEngine;

public class DustCloud : MonoBehaviour
{
    private Transform playerCamera;

    private void Update()
    {
        playerCamera = GameObject.FindWithTag("MainCamera").transform;
        transform.LookAt(playerCamera);
    }

    private void Disappeared()
    {
        Destroy(gameObject);
    }
}
