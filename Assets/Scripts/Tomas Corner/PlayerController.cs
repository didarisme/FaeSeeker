using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
/*
Contributor log: 
    -Tomas (9/17/2024)

Use Case: Recieve input, and affect the activePawn.
*/
public class PlayerController : MonoBehaviour
{
    public IPawn activePawn;
    public PlayerInputHandler inputHandler;
    private void Start(){
        if(inputHandler == null){
            Debug.LogError("ERROR: please pick a valid inputHandler");
        }
    }

    private void Update(){

    }

    private void HandleMovement(){
        
    }

    
    
} 
