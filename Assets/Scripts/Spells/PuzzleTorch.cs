using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleTorch : MonoBehaviour
{
    [SerializeField]Light torchLight;
    [SerializeField]ParticleSystem particles;
    private bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
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
        torchLight.enabled = onoff;
        if(onoff){
            particles.Play();
        }
        else{
            particles.Stop();
        }
        activated = false;
    }
}
