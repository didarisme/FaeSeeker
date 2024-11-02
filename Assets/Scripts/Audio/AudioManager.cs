using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource[] originalSources;
    [SerializeField] private SoundAudioClip[] soundAudioClipArray;
    [SerializeField] private AudioClip meowSound;
    [SerializeField] private int poolSize = 10;

    private Queue<AudioSource> audioPool = new Queue<AudioSource>();

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundType soundType;
        public AudioClip[] audioClips;
    }
    
    public enum SoundType
    {
        PlayerFootsteps,
        PlayerAttack,
        EnemyHit,
        EnemyDie,
        EnvEffect,
        ButtonClick
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject soundObject = new GameObject("PooledSound" + i);
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.volume = 0.5f;
            soundObject.transform.parent = transform;
            soundObject.SetActive(false);
            audioPool.Enqueue(audioSource);
        }
    }

    public void PlayAudio(SoundType soundType, int soundId, Vector3 position)
    {
        if (audioPool.Count > 0)
        {
            AudioSource audioSource = audioPool.Dequeue();
            AudioClip clip = GetAudioClip(soundType, soundId);

            if (clip == null)
            {
                Debug.LogError("Failed to retrieve audio clip!");
                return;
            }

            audioSource.transform.position = position;
            audioSource.gameObject.SetActive(true);
            audioSource.clip = clip;
            audioSource.Play();

            StartCoroutine(DeactivateAfterPlay(audioSource, audioSource.clip.length));
        }
        else
        {
            Debug.LogWarning("No audio sources available in the pool!");
        }
    }

    public void PlayAudio(SoundType soundType, int soundId, int audioSourceId)
    {
        if (audioSourceId < 0 || audioSourceId >= originalSources.Length)
        {
            Debug.LogError("Outside of sources array!");
            return;
        }

        AudioClip clip = GetAudioClip(soundType, soundId);

        if (clip == null)
        {
            Debug.LogError("Failed to retrieve audio clip!");
            return;
        }

        originalSources[audioSourceId].PlayOneShot(clip);
    }

    private AudioClip GetAudioClip(SoundType soundType, int soundId)
    {
        foreach (SoundAudioClip soundAudioClip in soundAudioClipArray)
        {
            if (soundAudioClip.soundType == soundType)
            {
                if (soundId < 0 || soundId >= soundAudioClip.audioClips.Length)
                {
                    Debug.LogError("Outside of clips array MEOW!");
                    return meowSound;
                }

                return soundAudioClip.audioClips[soundId];
            }
        }

        Debug.LogError("This " + soundType + " type was not found MEOW!");
        return meowSound;
    }

    private IEnumerator DeactivateAfterPlay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.gameObject.SetActive(false);
        audioPool.Enqueue(audioSource);
    }

    private void OnDestroy()
    {
        audioPool.Clear();
    }
}