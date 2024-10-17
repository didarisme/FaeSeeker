using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Animator animator;
    public int damage;
    private bool attacking = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (attacking)
        {
            if(other.tag == "Enemy")
            // Handle collision while attacking
            other.GetComponent<Mortality>().TakeDamage(damage);
        }
    }

    // Input
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
