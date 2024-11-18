using System;
using UnityEngine;
using TMPro;

public class HoverText : MonoBehaviour
{
    [SerializeField] private RectTransform hoverTransform;

    private TextMeshProUGUI textField;
    private bool isActive;

    public static Action<string> OnHoverText;

    private void Awake()
    {
        textField = hoverTransform.GetComponentInChildren<TextMeshProUGUI>();
        hoverTransform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnHoverText += SetHover;   
    }

    private void OnDisable()
    {
        OnHoverText -= SetHover;
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 mousePosition = Input.mousePosition;

            Vector3 hoverPosition = mousePosition - screenCenter + (Vector2.up * 15f);

            hoverTransform.anchoredPosition = hoverPosition;
        }
    }

    private void SetHover(string message)
    {
        if (message != null)
        {
            isActive = true;
            textField.text = message;
            hoverTransform.gameObject.SetActive(true);
        }
        else
        {
            hoverTransform.gameObject.SetActive(false);
            textField.text = "?";
            isActive = false;
        }
    }
}