using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private AudioSource audioToStop;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = objectToActivate.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            if(audioToStop!=null) audioToStop.Stop();
            Time.timeScale = 0f;
            objectToActivate.SetActive(true);
            audioSource.ignoreListenerPause = true;
            audioSource.Play();
        }
        
    }
}