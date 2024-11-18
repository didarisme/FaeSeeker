using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float pushPower = 1f;
    [SerializeField] private float offset = 0.1f;
    [SerializeField] private PlayerCamera playerCamera;

    private Transform model;
    private Animator anim;

    private bool shouldPushObject;
    private Rigidbody objectRigidbody;
    private Vector3 forceDirection;

    private void Awake()
    {
        model = GetComponentInChildren<PlayerAnimation>().transform;
        anim = GetComponentInChildren<Animator>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null && Mathf.Round(model.eulerAngles.y / 10) * 10 % 90 == 0)
        {
            objectRigidbody = rigidbody;
            forceDirection = hit.moveDirection;
            forceDirection.y = 0;

            shouldPushObject = true;
        }
    }

    private void FixedUpdate()
    {
        if (shouldPushObject)
        {
            Vector3 playerPos = transform.position;
            Vector3 pushObject = objectRigidbody.transform.position;

            if (Mathf.Abs(forceDirection.x) > Mathf.Abs(forceDirection.z))
            {
                forceDirection.z = 0;

                if (Mathf.Abs(playerPos.z - pushObject.z) > offset)
                {
                    shouldPushObject = false;
                    return;
                }
            }
            else
            {
                forceDirection.x = 0;

                if (Mathf.Abs(playerPos.x - pushObject.x) > offset)
                {
                    shouldPushObject = false;
                    return;
                }
            }

            objectRigidbody.AddForceAtPosition(forceDirection.normalized * pushPower, transform.position, ForceMode.Impulse);

            anim.SetBool("IsPushing", true);

            if (playerCamera != null)
                playerCamera.SetUpdateMode(false);

            CancelInvoke(nameof(ResetAnimation));
            Invoke(nameof(ResetAnimation), 0.1f);

            shouldPushObject = false;
        }
    }

    private void ResetAnimation()
    {
        if (playerCamera != null)
            playerCamera.SetUpdateMode(true);

        anim.SetBool("IsPushing", false);
    }
}
