using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortality : MonoBehaviour
{
    public int Health { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Health = 100; // Initialize health to 100 or any desired value
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
        if(Health == 0){
            Destroy(gameObject);
        }
    }

}
