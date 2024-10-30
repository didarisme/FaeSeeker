using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Animator animator;
    public int damage;
    private bool attacking = false; 

    //Sword collision
    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (attacking)
        print("Slashed: "+ other.name);
        {
            if(other.tag == "Enemy")
            {
                // Handle collision while attacking
                other.GetComponent<Mortality>().TakeDamage(damage);
                other.GetComponent<Rigidbody>().AddForce(transform.forward * damage, ForceMode.Impulse);
            }
            
        }
    }

    // Temporary input functionality
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(!attacking){
                StartAttack();
            }
        }
    }

    public void StartAttack(){
        animator.SetTrigger("attackTrigger");
        StartCoroutine(AttackCooldown());
    }
    IEnumerator AttackCooldown()
    {
        attacking = true;
        yield return new WaitForSeconds(1f);
        attacking=false;
    }

}
