using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float cameraMoveSpeed = 1f;
    [SerializeField] private float moveAmount = 4f;
    [SerializeField] private float edgeSize = 30f; 

    private Vector3 cameraFollowPosition;
    private bool isLateUpdate = true;

    private void LateUpdate()
    {
        if (!isLateUpdate) return;

        ReTargetPosition();
        CameraFollow(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (isLateUpdate) return;

        ReTargetPosition();
        CameraFollow(Time.fixedDeltaTime);
    }

    private void ReTargetPosition()
    {
        cameraFollowPosition = targetObject.position;

        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraFollowPosition.x += moveAmount;
        }
        if (Input.mousePosition.x < edgeSize)
        {
            cameraFollowPosition.x -= moveAmount;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.z += moveAmount;
        }
        if (Input.mousePosition.y < edgeSize)
        {
            cameraFollowPosition.z -= moveAmount;
        }
    }

    private void CameraFollow(float deltaTime)
    {
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveSpeed * distance * deltaTime * cameraMoveDir;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }

    public void SetUpdateMode(bool isLate)
    {
        isLateUpdate = isLate;
    }
}