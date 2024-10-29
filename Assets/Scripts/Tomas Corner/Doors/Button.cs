using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door target;
    public GameObject keyObject;
    public List<GameObject> objectsInRadius;
    void OnTriggerEnter(Collider other){
        if(keyObject != null){
            if(other.gameObject == keyObject){
                Register(other.gameObject);
                target.Open();
            }
        }
        else if(other.tag == "Interactable" || other.tag == "Player"){
            Register(other.gameObject);
            target.Open();
            
        }
    }

    void OnTriggerExit(Collider other){
        if(keyObject != null){
            if(other.gameObject == keyObject){
                Deregister(other.gameObject);
                if(objectsInRadius.Count == 0){
                    target.Close();
                }
            }
        }
        else if(other.tag == "Interactable" || other.tag == "Player"){
            Deregister(other.gameObject);
            if(objectsInRadius.Count == 0){
                target.Close();
            }
            
        }
    }

    private void Register(GameObject incoming){
        objectsInRadius.Add(incoming);
    }
    private void Deregister(GameObject outgoing){
        objectsInRadius.Remove(outgoing);
    }

}
