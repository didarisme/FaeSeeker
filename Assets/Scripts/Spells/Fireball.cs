using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    private FireballFactory factory;
    [SerializeField] private float speed = 10;
    [SerializeField] private float duration = 2f;
    private float lifespan = 0f;

    private void Start()
    {
        factory = FindObjectOfType<FireballFactory>();
    }
    private void OnEnable()
    {
        lifespan = duration;
    }

    // Update is called once per frame
    void Update()
    {
        FlyForward();
        CountDown();
    }

    private void FlyForward(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void CountDown(){
        lifespan -= Time.deltaTime;
        if(lifespan <= 0f){
            Explode();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            //Explode();
        }
    }
    private void OnTriggerEnter(){
        //Explode();
    }

    private void Explode(){
        factory.RemoveFireball(gameObject);
        //Play particle system effect
    }
}
