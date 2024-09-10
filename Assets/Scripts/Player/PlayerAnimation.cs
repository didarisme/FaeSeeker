using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSmoothTime = 0.5f;
    private float rotationVelocity = 0;

    private PlayerMove playerMove;
    private Animator animator;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        playerMove.OnMovement += ApplyAnimtaions;
        playerMove.OnStateChange += ChangeAnimationState;
    }

    private void OnDisable()
    {
        playerMove.OnMovement -= ApplyAnimtaions;
        playerMove.OnStateChange -= ChangeAnimationState;
    }

    private void ApplyAnimtaions(Vector2 currentInput, bool isGrounded)
    {
        if (currentInput != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(currentInput.x, currentInput.y) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        if (isGrounded)
        {
            animator.SetFloat("Speed", currentInput.magnitude);
        }
    }

    private void ChangeAnimationState()
    {
        if (playerMove.CurrentState == PlayerMove.MovementState.crouchning)
        {
            animator.SetBool("IsCrouching", true);
        }
        else
        {
            animator.SetBool("IsCrouching", false);
        }
    }
}