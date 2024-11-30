using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReloadList : MonoBehaviour
{
    private Dictionary<GameObject,int> objectsToReload;
    
    //Reloads objects into their starting state and position.
    public abstract void Reload();

    public void RegisterObject(GameObject obj, int section){
        objectsToReload.Add(obj,section);
    }

    // Called when an object doesnt need to be tracked anymore
    // Example: checkpoint reached and everything before it will not be reloaded.
    public void UnRegisterSection(int checkpointSection){
        foreach(KeyValuePair<GameObject,int> objSection in objectsToReload){
            //if section is cleared
            if(objSection.Value == checkpointSection){
                UnRegisterObject(objSection.Key);
            }
        }
    }

    private void UnRegisterObject(GameObject obj){
        objectsToReload.Remove(obj);
    }

}
