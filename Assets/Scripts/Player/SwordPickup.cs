using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    [SerializeField] GameObject swordModel;
    [SerializeField] DaxForms forms;

    private void Start()
    {
        swordModel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            swordModel.SetActive(true);
            forms.ChangeForm(DaxForms.FormType.Physical);
            Destroy(gameObject);
        }
    }
}
