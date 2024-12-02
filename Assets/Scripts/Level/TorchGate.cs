using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGate : MonoBehaviour
{
    [SerializeField] private Transform gate;
    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private float gateSpeed = 1.5f;
    List<PuzzleTorch> torches;
    private void Awake()
    {
        torches = new List<PuzzleTorch>();
        closedPosition = gate.localPosition;

    }
    public void RegisterTorch(PuzzleTorch torch){
        torches.Add(torch);
    }
    public void CheckTorches(){
        foreach(PuzzleTorch torch in torches){
            if (!torch.IsActive()){
                CloseDoor();
                return; // We need all torches to be active to open door
            }
        }
        OpenDoor();
    }

    private void OpenDoor(){
        StopAllCoroutines();
        StartCoroutine(MoveGate(openPosition));
    }
    private void CloseDoor(){
        StopAllCoroutines();
        StartCoroutine(MoveGate(closedPosition));
    }

    private IEnumerator MoveGate(Vector3 targetPos)
    {
        while (Vector3.Distance(gate.localPosition, targetPos) > 0.01f)
        {
            gate.localPosition = Vector3.MoveTowards(gate.localPosition, targetPos, gateSpeed * Time.deltaTime);
            yield return null;
        }

        gate.localPosition = targetPos;
    }
}
