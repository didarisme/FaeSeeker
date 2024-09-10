using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool IsSprinting => canSprint && !isCrouching && currentInput.magnitude != 0 && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && !isCrouching && isGrounded;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 8f;
    [SerializeField] private float smoothTime = 0.5f;
    private float currentVelocity;
    private float speed;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 30f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 1.35f;
    [SerializeField] private float standingHeight = 1.8f;
    [SerializeField] private float timeToCroach = 0.5f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.675f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0.925f, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Ground Parameters")]
    [SerializeField] private float GroundedOffset = 0.14f;
    [SerializeField] private float GroundedRadius = 0.4f;
    [SerializeField] private LayerMask GroundLayers;
    private bool isGrounded = true;

    private CharacterController characterController;

    private Vector3 hitPointNormal;

    private Vector3 moveDirection;
    private Vector3 slideDirection;
    private Vector2 currentInput;

    private bool isSliding
    {
        get
        {
            if (isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    public event Action<Vector2, bool> OnMovement;
    public event Action OnStateChange;

    private MovementState state;
    public MovementState CurrentState { get; private set; }

    public enum MovementState
    {
        walking,
        running,
        crouchning
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        OnStateChange?.Invoke();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleStates();
        GroundedCheck();

        if (canJump)
            HandleJump();

        if (canCrouch)
            HandleCrouch();

        ApplyFinalMovements();
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
        float targetSpeed = isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed;

        if (currentInput == Vector2.zero)
        {
            targetSpeed = 0;
        }

        if (currentSpeed < targetSpeed - 0.1f)
        {
            speed = Mathf.SmoothDamp(speed, targetSpeed, ref currentVelocity, smoothTime);
        }
        else
        {
            speed = targetSpeed;
        }

        float moveDirectionY = moveDirection.y;
        moveDirection = ((currentInput.y * transform.TransformDirection(Vector3.forward)) + (currentInput.x * transform.TransformDirection(Vector3.right))) * speed;
        moveDirection.y = moveDirectionY;
    }

    private void HandleStates()
    {
        state = isCrouching ? MovementState.crouchning : IsSprinting ? MovementState.running : MovementState.walking;

        if (state != CurrentState)
        {
            CurrentState = state;
            OnStateChange?.Invoke();
        }
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private void ApplyFinalMovements()
    {
        if (!isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        if (isSliding)
        {
            slideDirection += slopeSpeed * Time.deltaTime * new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z);
            moveDirection += slideDirection;
        }
        else if (moveDirection.y < -4f && isGrounded)
        {
            moveDirection.y = -4f;
            slideDirection = Vector3.zero;
        } //FIX: when on slope change moveDirection

        OnMovement?.Invoke(currentInput * speed, isGrounded);
        characterController.Move(Time.deltaTime * moveDirection);
    }

    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(new Vector3(0, crouchHeight, 0), Vector3.up, standingHeight - crouchHeight))
            yield break;

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        isCrouching = !isCrouching;

        while (timeElapsed < timeToCroach)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCroach);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCroach);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        duringCrouchAnimation = false;
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z), GroundedRadius);
    }
}