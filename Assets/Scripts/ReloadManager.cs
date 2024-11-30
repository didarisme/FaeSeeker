using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    List<ReloadList> reloadLists;

    public void CheckpointReached(int checkpointSection, Vector3 respawn){
        foreach(ReloadList list in reloadLists){
            list.UnRegisterSection(checkpointSection);
        }
        ResetPlayer(respawn);
    }

    public void Reload(){
        foreach(ReloadList list in reloadLists){
            list.Reload();
        }
    }

    private void ResetPlayer(Vector3 respawn){
        //TODO
    }
}
