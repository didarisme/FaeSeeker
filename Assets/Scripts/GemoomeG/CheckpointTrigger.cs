using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]Transform spawnPoint;
    public int section;
    ReloadManager reloadManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            reloadManager.CheckpointReached(section, spawnPoint.position);
        }
    }
}
