using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaxForms : MonoBehaviour
{
    public enum FormType
    {
        Magical,
        Physical,
        Neutral
    }
    [SerializeField] private Transform playerLocal;
    [SerializeField]private FormType currentForm = FormType.Physical;
    [SerializeField]private FireballFactory fireballFactory;
    [SerializeField]private PlayerAttack attack;
    [SerializeField]private KeyCode attackKey = KeyCode.Mouse0;
    
    //Delegate function for hot swapping attacks
    private delegate void FormAction();
    private FormAction currentAction;
    
    void Start()
    {
        // Initialize the current action based on the initial form
        ChangeForm(currentForm);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(attackKey)){
            print("Pressing attack");
            currentAction?.Invoke();
        }
    }

    void MagicInput()
    {
        print("Casting");
        fireballFactory.SpawnFireball(playerLocal);
    }

    void PhysicalInput()
    {
        print("Attacking");
        attack.TryToAttack();
    }

    public void ChangeForm(FormType form){
        currentForm = form;
        switch(currentForm)
        {
            case FormType.Magical:
                currentAction = MagicInput;
                break;
            case FormType.Physical:
                currentAction = PhysicalInput;
                break;
            case FormType.Neutral:
                currentAction = null;
                break;
        }
    } 
}
