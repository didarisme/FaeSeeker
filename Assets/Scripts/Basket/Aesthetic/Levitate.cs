using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float amplitude = 0.2f;
    [SerializeField] private float yOffset;
 
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Levitation();
    }
    private void Levitation()
    {
        timer += Time.deltaTime * speed;

        transform.position = new Vector3(transform.position.x, yOffset + Mathf.PingPong(timer * amplitude, amplitude), transform.position.z);
    }
}
