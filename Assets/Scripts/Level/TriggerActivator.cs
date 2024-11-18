using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = objectToActivate.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
        objectToActivate.SetActive(true);
        audioSource.ignoreListenerPause = true;
        audioSource.Play();
    }
}