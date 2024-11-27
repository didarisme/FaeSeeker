using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPickup : MonoBehaviour
{
    [SerializeField] GameObject gunModel;
    [SerializeField] DaxForms forms;

    private void Start()
    {
        //gunModel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gunModel.SetActive(true);
            //forms.ChangeForm(DaxForms.FormType.Magical);
            Destroy(gameObject);
        }
    }
}
