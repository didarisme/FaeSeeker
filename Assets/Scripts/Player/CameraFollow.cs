using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 10, -10);
    public float smoothTime = 0.25f;
    public float rotationSpeed = 100.0f; // Speed of rotation
    public float zoomSpeed = 2.0f; // Speed of zooming
    public float minZoom = 5.0f; // Minimum zoom distance
    public float maxZoom = 20.0f; // Maximum zoom distance

    private Vector3 currentVelocity;
    private float yaw;

    private void LateUpdate()
    {
        // Capture input for rotation
        if (Input.GetKey(KeyCode.Q))
        {
            yaw -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            yaw += rotationSpeed * Time.deltaTime;
        }

        // Capture input for zoom
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        offset = offset * (1 - scrollInput * zoomSpeed);
        offset = Vector3.ClampMagnitude(offset, maxZoom);
        if (offset.magnitude < minZoom)
        {
            offset = offset.normalized * minZoom;
        }

        // Calculate new rotation
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);

        // Calculate new position
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);

        // Look at the target
        transform.LookAt(target.position);
    }
}
