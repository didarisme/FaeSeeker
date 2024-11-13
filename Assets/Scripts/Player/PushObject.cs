using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float pushPower = 1f;
    [SerializeField] private float offset = 0.1f;
    [SerializeField] private PlayerCamera playerCamera;

    private Transform model;
    private Animator anim;

    private void Awake()
    {
        model = GetComponentInChildren<PlayerAnimation>().transform;
        anim = GetComponentInChildren<Animator>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        float rotation = model.eulerAngles.y;

        if (rigidbody != null && Mathf.Round(rotation / 10) * 10 % 90 == 0)
        {
            Vector3 playerPos = transform.position;
            Vector3 pushObject = hit.transform.position;

            Vector3 forceDirection = hit.moveDirection;
            forceDirection.y = 0;

            if (Mathf.Abs(forceDirection.x) > Mathf.Abs(forceDirection.z))
            {
                forceDirection.z = 0;

                if (Mathf.Abs(playerPos.z - pushObject.z) > offset) return;
            }
            else
            {
                forceDirection.x = 0;

                if (Mathf.Abs(playerPos.x - pushObject.x) > offset) return;
            }

            rigidbody.AddForceAtPosition(forceDirection.normalized * pushPower, transform.position, ForceMode.Impulse);

            anim.SetBool("isPushing", true);

            if (playerCamera != null)
                playerCamera.SetUpdateMode(false);

            CancelInvoke(nameof(ResetAnimation));
            Invoke(nameof(ResetAnimation), 0.1f);
        }
    }

    private void ResetAnimation()
    {
        if (playerCamera != null)
            playerCamera.SetUpdateMode(true);

        anim.SetBool("isPushing", false);
    }
}