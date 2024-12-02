using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleTorch : MonoBehaviour
{
    [SerializeField]TorchGate torchGate;
    [SerializeField]Light torchLight;
    [SerializeField]ParticleSystem particles;
    private bool activated = false;

    void Start()
    {
        //print(torchGate.gameObject.name);
        torchGate.RegisterTorch(this);
        LightSwitch(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Fireball>() != null)
        {
            LightSwitch(true);
        }
    }
    public void LightSwitch(bool onoff){ 
        if(onoff){
            particles.Play();
        }
        else{
            particles.Stop();
        }
        activated = onoff;
        torchLight.enabled = onoff;
        NotifyDoor();
    }

    public void NotifyDoor(){
        torchGate.CheckTorches();
    }

    public bool IsActive(){
        return activated;
    }
}
