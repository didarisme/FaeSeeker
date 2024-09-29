using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnDax : Pawn
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 30.0f;
    
    public override void Exorcise()
    {
        // Add your implementation here
    }
    public override void Possess()
    {
        // Add your implementation here
    }
    
    public override void Move(Vector2 moveInput, float sprintValue)
    {
        // Add your implementation here
        float speed = walkSpeed * (sprintValue > 0 ? sprintMultiplier : 1.0f);

        Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x*speed;
        currentMovement.z = worldDirection.z*speed;
        if (characterController.isGrounded){
            currentMovement.y = -0.5f;
        }
        else{
            currentMovement.y -= gravity * Time.deltaTime;
        }
        
        characterController.Move(currentMovement * Time.deltaTime);
    }

    public override void Jump(){
        currentMovement.y = jumpForce;
    }

}


