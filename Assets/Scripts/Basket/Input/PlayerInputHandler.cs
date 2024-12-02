using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Singleton class for recieving hardware inputs.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Input Map Name References")]
    [SerializeField] private string actionMapName = "Dax";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string hide = "Hide";
    [SerializeField] private string fly = "Fly";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction; 
    private InputAction hideAction;
    private InputAction flyAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public float HideValue { get; private set; }
    public bool FlyTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }
    private void Awake()
    {
        //Ensure object is singleton
        if (Instance==null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        //Bind actions to their mapped values
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        hideAction = playerControls.FindActionMap(actionMapName).FindAction(hide);
        flyAction = playerControls.FindActionMap(actionMapName).FindAction(fly);

        RegisterInputActions();
    }

    void RegisterInputActions(){
        //Move
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        //Look
        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;
        //Jump
        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;
        //Sprint
        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;
        //Hide
        hideAction.performed += context => HideValue = context.ReadValue<float>();
        hideAction.canceled += context => HideValue = 0f;

        

    }

    private void OnEnable(){
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        hideAction.Enable();

    }

    private void OnDisable(){
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        hideAction.Disable();
    }
}
