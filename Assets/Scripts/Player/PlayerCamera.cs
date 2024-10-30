using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float cameraMoveSpeed = 1f;
    [SerializeField] private float moveAmount = 4f;

    private void LateUpdate()
    {
        float edgeSize = 30f;
        Vector3 cameraFollowPosition = targetObject.position;

        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraFollowPosition.x += moveAmount;
            Debug.Log("Move Right");
        }
        if (Input.mousePosition.x < edgeSize)
        {
            cameraFollowPosition.x -= moveAmount;
            Debug.Log("Move Left");
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.z += moveAmount;
            Debug.Log("Move Up");
        }
        if (Input.mousePosition.y < edgeSize)
        {
            cameraFollowPosition.z -= moveAmount;
            Debug.Log("Move Down");
        }


        HubaBubbaMove(cameraFollowPosition);
    }

    private void HubaBubbaMove(Vector3 cameraFollowPosition)
    {
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }
}