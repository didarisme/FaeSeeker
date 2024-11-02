using UnityEngine;

public class CameraTesting : MonoBehaviour
{
    [SerializeField] private Transform[] targets;

    private PlayerCamera playerCamera;
    int currentTarget;

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
    }

    public void ChangeTarget()
    {
        currentTarget++;

        if (currentTarget >= targets.Length)
            currentTarget = 0;

        playerCamera.SetNewTarget(targets[currentTarget]);
    }
}