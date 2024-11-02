using UnityEngine;

public class AudioTester : MonoBehaviour
{
    public void LeftBtn()
    {
        AudioManager.Instance.PlayAudio(AudioManager.SoundType.EnvEffect, 0, 0);
    }

    public void RightBtn()
    {
        AudioManager.Instance.PlayAudio(AudioManager.SoundType.EnvEffect, 0, 1);
    }

    public void RandomBtn()
    {
        Vector2 randomPos = Random.insideUnitSphere * 8f;
        AudioManager.Instance.PlayAudio(AudioManager.SoundType.EnvEffect, 1, new Vector3(randomPos.x, 0, randomPos.y));
    }
}