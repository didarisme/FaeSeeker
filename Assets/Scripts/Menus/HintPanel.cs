using System;
using UnityEngine;
using TMPro;

public class HintPanel : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private bool showOnStart = true;

    private PlayerCamera playerCamera;
    private CanvasGroup hintGroup;

    private bool targetChanged;

    public static Action<string, Transform> OnHint;

    private void OnEnable()
    {
        OnHint += ShowHint;
    }

    private void OnDisable()
    {
        OnHint -= ShowHint;
    }

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        hintGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (showOnStart)
        {
            DisplayHintGroup(true);
            BlockMovement(true);

        }
        else
        {
            DisplayHintGroup(false);
            BlockMovement(false);
        }
    }

    private void ShowHint(string hintText, Transform newCameraTarget)
    {
        DisplayHintGroup(true);
        BlockMovement(true);

        if (newCameraTarget != null)
        {
            playerCamera.SetNewTarget(newCameraTarget);
            targetChanged = true;
        }

        textField.text = hintText;
    }

    public void HideHint()
    {
        DisplayHintGroup(false);
        BlockMovement(false);

        if (targetChanged)
        {
            playerCamera.SetNewTarget(player);
            targetChanged = false;
        }

        textField.text = "...";
    }

    private void DisplayHintGroup(bool display)
    {
        if (display)
        {
            hintGroup.alpha = 1f;
            hintGroup.interactable = true;
            hintGroup.blocksRaycasts = true;
        }
        else
        {
            hintGroup.alpha = 0f;
            hintGroup.interactable = true;
            hintGroup.blocksRaycasts = false;
        }
    }

    private void BlockMovement(bool isDisabled)
    {
        if (isDisabled)
            player.GetComponentInChildren<PlayerAnimation>().ResetAnimations();

        player.GetComponent<PlayerMove>().enabled = !isDisabled;
        player.GetComponent<PlayerAttack>().enabled = !isDisabled;
    }
}