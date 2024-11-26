using UnityEngine;
using UnityEngine.Pool;

// Note that this class is not a MonoBehavior. Just a pure C# class.
public class GameObjectPool
{
    GameObject prefab;
    ObjectPool<GameObject> pool;
    int defaultSize;
    int maxSize;

    // Our class's constructor. Takes the prefab to spawn as an argument.
    public GameObjectPool(GameObject prefab, int defaultSize=10, int maxSize=20)
    {
        this.prefab = prefab;  // The prefab to spawn.
        this.defaultSize = defaultSize;
        this.maxSize = maxSize;
        // Initializing our pool. This won't work; it's missing some fields.
        pool = new ObjectPool<GameObject>(
            CreatePooledObject,     //createFunc
            OnGetFromPool,          //actionOnGet
            OnReturnToPool,         //actionOnRelease
            OnDestroyPooledObject,  //actionOnDestroy
            true,                   //collectionCheck
            defaultSize,            //defaultCapacity
            maxSize                 //maxSize
        );
        
    }
    //Returns an object from the pool
    public GameObject GetObject(Vector3 position)
    {
        GameObject obj = pool.Get();
        obj.transform.position = position;
        return obj;
    }

    public void ReleaseObject(GameObject obj)
    {
        pool.Release(obj);
    }
    
    private GameObject CreatePooledObject()
    {
        GameObject newObject = GameObject.Instantiate(prefab);
        return newObject;
    }

    private void OnGetFromPool(GameObject pooledObject)
    {
        pooledObject.SetActive(true);
    }
    private void OnReturnToPool(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
    }
    private void OnDestroyPooledObject(GameObject pooledObject)
    {
        GameObject.Destroy(pooledObject);
    }
    
    
}