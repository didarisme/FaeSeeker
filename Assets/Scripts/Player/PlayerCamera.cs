using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform cameraTranform;

    [Header("Camera Parameters")]
    [SerializeField] private Vector2 distanceRange = new Vector2(6f, 8f);
    [SerializeField] private float angle = 60f;
    [SerializeField] private float zoomTime = 0.5f;
    [SerializeField] private float cameraMoveSpeed = 2f;
    [SerializeField] private float moveAmount = 1.5f;
    [SerializeField] private float edgeSize = 30f;

    [Header("Controls")]
    [SerializeField] private KeyCode zoomKey = KeyCode.V;

    private Vector3 cameraFollowPosition;
    private bool isLateUpdate = true;
    private bool isLerping;
    private bool isZoomed;

    private void Awake()
    {
        CameraAngle(distanceRange.y, angle, true);
    }

    private void LateUpdate()
    {
        if (!isLateUpdate) return;

        UpdateCamera(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (isLateUpdate) return;

        UpdateCamera(Time.fixedDeltaTime);
    }

    private void UpdateCamera(float deltaTime)
    {
        ReTargetPosition();
        CameraFollow(deltaTime);
        Zooming();
    }

    private void Zooming()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            if (isLerping) return;

            isZoomed = !isZoomed;

            if (isZoomed)
                CameraAngle(distanceRange.x, angle, false);
            else
                CameraAngle(distanceRange.y, angle, false);
        }
    }

    private void ReTargetPosition()
    {
        cameraFollowPosition = targetObject.position;

        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraFollowPosition.x += moveAmount;
        }
        else if (Input.mousePosition.x < edgeSize)
        {
            cameraFollowPosition.x -= moveAmount;
        }

        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.z += moveAmount;
        }
        else if (Input.mousePosition.y < edgeSize)
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

    public void CameraAngle(float hypotenuse, float angleInDegrees, bool instantly)
    {
        if (hypotenuse <= 0)
        {
            hypotenuse = distanceRange.y;
            angleInDegrees = angle;
        }

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        float adjacent = hypotenuse * Mathf.Cos(angleInRadians);
        float opposite = hypotenuse * Mathf.Sin(angleInRadians);

        if (instantly)
            cameraTranform.SetLocalPositionAndRotation(new Vector3(0, opposite, -adjacent), Quaternion.Euler(new Vector3(angleInDegrees, 0, 0)));
        else
            StartCoroutine(MoveToPosRot(new Vector3(0, opposite, -adjacent), angleInDegrees, zoomTime));
    }

    public void SetUpdateMode(bool isLate)
    {
        isLateUpdate = isLate;
    }

    public void SetNewTarget(Transform newTargetObject)
    {
        targetObject = newTargetObject;
    }

    private IEnumerator MoveToPosRot(Vector3 targetPos, float targetAngle, float duration)
    {
        isLerping = true;

        Vector3 startPos = cameraTranform.localPosition;
        float startRot = cameraTranform.localRotation.eulerAngles.x;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cameraTranform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);

            if (targetAngle != startRot)
                cameraTranform.localRotation = Quaternion.Euler(Mathf.Lerp(startRot, targetAngle, elapsedTime / duration), 0, 0);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        if (targetAngle != startRot)
            cameraTranform.localRotation = Quaternion.Euler(targetAngle, 0, 0);

        cameraTranform.localPosition = targetPos;

        isLerping = false;
    }
}