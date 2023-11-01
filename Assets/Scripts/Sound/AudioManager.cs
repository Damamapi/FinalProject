using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds, clickSounds;

    [Space]

    public AudioSource musicSource, sfxSource;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayWorldTheme();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayWorldTheme();
    }

    void PlayWorldTheme()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string themeToPlay = null;

        if (currentSceneName.Contains("World1"))
        {
            themeToPlay = "world1";
        }
        else if (currentSceneName.Contains("World2"))
        {
            themeToPlay = "world2";
        }

        if (themeToPlay != null)
        {
            PlayMusic(themeToPlay);
        }
        else
        {
            Debug.LogWarning("No theme specified for the current scene: " + currentSceneName);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
        }
        else if (musicSource.clip != s.clip || !musicSource.isPlaying)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayRandomClick()
    {
        int click = UnityEngine.Random.Range(0, clickSounds.Length);
        sfxSource.PlayOneShot(clickSounds[click].clip);
    }
}
