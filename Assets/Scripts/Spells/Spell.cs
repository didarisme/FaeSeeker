using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField]private int manaCost;

    //abstract public void Cast();

    public int getManaCost(){
        return manaCost;
    }
}
