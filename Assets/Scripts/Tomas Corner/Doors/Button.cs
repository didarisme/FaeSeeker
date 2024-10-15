using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door target;
    void OnTriggerEnter(Collider other){
        if(other.tag == "Interactable"){
            target.Open();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Interactable"){
            target.Close();
        }
    }

}
