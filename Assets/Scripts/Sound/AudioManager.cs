using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private Dictionary<string, AudioClip> audioClipDictionary;
    private AudioSource audioSource;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioClipDictionary = new Dictionary<string, AudioClip>();

        foreach (Sound sound in sounds)
        {
            audioClipDictionary.Add(sound.name, sound.clip);
        }
    }

    public void PlaySound(string name)
    {
        if (audioClipDictionary.TryGetValue(name, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void PlayLoopSound(string name)
    {
        if (audioClipDictionary.TryGetValue(name, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }
}
