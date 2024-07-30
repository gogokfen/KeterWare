using UnityEngine;

public class SFXController : MonoBehaviour
{
    private static SFXController instance;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void PlaySFX(string clipName, float volume = 1.0f, bool loop = false)
    {
        if (instance != null && instance.audioSource != null && !string.IsNullOrEmpty(clipName))
        {
            string path = $"Sounds/Sfx/{clipName}";
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip != null)
            {
                if (loop)
                {
                    instance.audioSource.clip = clip;
                    instance.audioSource.volume = volume;
                    instance.audioSource.loop = true;
                    instance.audioSource.Play();
                }
                else
                {
                    instance.audioSource.PlayOneShot(clip, volume);
                }
            }
            else
            {
                Debug.Log($"AudioClip '{clipName}' not found at '{path}' in Resources.");
            }
        }
        else
        {
            Debug.Log("SFXController or AudioSource or clipName is missing.");
        }
    }

    public static void StopSFX()
    {
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.Stop();
            instance.audioSource.loop = false;
        }
        else
        {
            Debug.Log("SFXController or AudioSource is missing.");
        }
    }
}
