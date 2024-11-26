using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class FireballFactory : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject fireballPrefab;
    private GameObjectPool fireballPool;
    // Start is called before the first frame update
    void Start()
    {
        fireballPool = new GameObjectPool(fireballPrefab, poolSize, poolSize*2);
    }

    public void SpawnFireball(Transform target){
        GameObject fireball = fireballPool.GetObject(target.position);
        fireball.transform.rotation = target.rotation;
    }

    public void RemoveFireball(GameObject fireball)
    {
        fireballPool.ReleaseObject(fireball);
    }

    //Testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnFireball(transform);
        }
    }

}
