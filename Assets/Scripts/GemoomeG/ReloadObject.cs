using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadObject : MonoBehaviour
{
    [SerializeField]private int section;
    [SerializeField]ReloadManager reloadManager;
    private Vector3 startingPostion;
    private Quaternion startingRotation;
    private void Start()
    {
        startingPostion = transform.position;
        startingRotation = transform.rotation;
    }
    private void Reload()
    {
        gameObject.transform.position = startingPostion;
        transform.rotation = startingRotation;
    }
}
