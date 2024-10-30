using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortality : MonoBehaviour
{
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void DisplayHealth(){

    }

}
