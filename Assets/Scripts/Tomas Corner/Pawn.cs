using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public interface IPawn
{
    public void Possess();
    public void Exorcise();
    public void Move(Vector2 moveInput, float sprintValue);
    public void Interact();
    public void UseItem();
    public void Sprint();
    public void Hide();
    public void Jump();
    public void Attack();
    public void Magic();
    public void Block();
    
}

public abstract class Pawn : MonoBehaviour,IPawn
{
    protected CharacterController characterController;
    protected CinemachineVirtualCamera mainCamera;
    protected Vector3 currentMovement;
    private void Start(){
        characterController = GetComponent<CharacterController>();
        if(characterController == null){
            Debug.LogError("ERROR: character controller needed in same gameObject as pawn");
        }
    }
    public abstract void Possess();
    public abstract void Exorcise();
    public abstract void Move(Vector2 moveInput, float sprintValue);
    public virtual void Interact(){}
    public virtual void UseItem(){}
    public virtual void Sprint(){}
    public virtual void Hide(){}
    public virtual void Jump(){}
    public virtual void Attack(){}
    public virtual void Magic(){}
    public virtual void Block(){}
    
}
