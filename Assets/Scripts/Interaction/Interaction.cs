using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    public static Action<bool> IsOnFocus;

    [Header("Controls")]
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    private void Update()
    {
        HandleInteractionCheck();
        HandleInteractionInput();
    }

    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionDistance) && hit.collider.gameObject.layer == 8)
        {
            // 8 - Interactable layer
            if (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID())
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                    IsOnFocus?.Invoke(true);
                }
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            IsOnFocus?.Invoke(false);
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractable.OnInteract();
        }
    }
}