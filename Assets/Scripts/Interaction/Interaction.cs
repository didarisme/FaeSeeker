using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private float interactionDistance = 20f;
    [SerializeField] private float playerDistance = 2f;
    [SerializeField] private LayerMask interactionLayer = default;

    [Header("Controls")]
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    private Transform playerTransform;

    private Interactable currentInteractable;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        HandleInteractionCheck();
        HandleInteractionInput();
        HandleInteractionCollect();
    }

    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionDistance) && hit.collider.gameObject.layer == 8)
        {
            // 8 - Interactable layer
            if (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID())
            {
                if (currentInteractable)
                {
                    currentInteractable.OnLoseFocus();
                    currentInteractable = null; 
                }

                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionCollect()
    {
        Collider[] collectableInRange = Physics.OverlapSphere(playerTransform.position, 1, interactionLayer);

        foreach (var collectableCollider in collectableInRange)
        {
            if (collectableCollider.TryGetComponent<Interactable>(out Interactable collectableObject))
            {
                collectableObject.OnCollect();
            }
        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            if (Vector3.Distance(playerTransform.position, currentInteractable.transform.position) < playerDistance)
                currentInteractable.OnInteract();
            else
                HoverText.OnHoverText?.Invoke("Too far");
        }
    }
}