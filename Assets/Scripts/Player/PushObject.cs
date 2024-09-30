using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField] private float pushPower = 1f;
    private float resetTimer = 0.1f;
    private bool isPushing;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isPushing)
        {
            ResetAnimation();
        }
    }

    private void ResetAnimation()
    {
        resetTimer += Time.deltaTime;

        if (resetTimer > 0.1f)
        {
            anim.SetBool("isPushing", false);
            isPushing = false;
            resetTimer = 0;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * pushPower, transform.position, ForceMode.Impulse);

            anim.SetBool("isPushing", true);
            isPushing = true;
            resetTimer = 0;
        }
    }
}