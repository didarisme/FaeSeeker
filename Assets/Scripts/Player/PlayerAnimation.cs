using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSmoothTime = 0.5f;

    private float rotationVelocity = 0;
    private float speed;

    private PlayerMove playerMove;
    private Animator animator;
    private Transform player;
    private Vector2 prevPos;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<PlayerMove>().transform;
        prevPos = new Vector2(player.position.x, player.position.z);
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

    private void ApplyAnimtaions(Vector2 currentInput, float targetRotation, bool isGrounded)
    {
        if (currentInput != Vector2.zero)
        {
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);

        if (isGrounded && prevPos != playerPos)
        {
            speed = Mathf.Lerp(speed, currentInput.magnitude, Time.deltaTime * 7f);
            animator.SetFloat("Speed", speed);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 3f);
            animator.SetFloat("Speed", speed);
        }

        prevPos = new Vector2(player.position.x, player.position.z);
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

    public void Attack(){
        //PLAY ATTACK ANIMATION HERE
    }
}