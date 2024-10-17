using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Animator animator;
    private bool attacking = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            StartAttack();
        }
    }

    private void StartAttack(){
        attacking = true;
        animator.SetTrigger("attackTrigger");
    }

    private void StopAttack(){

    }
}
