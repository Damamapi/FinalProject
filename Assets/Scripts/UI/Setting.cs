using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.PlaySFX("hihatOpen");
        SceneManager.LoadScene("Basic Level");
    }

    public void Return()
    {
        AudioManager.Instance.PlaySFX("hihatClosed");
        SceneManager.LoadScene("MainMenu");
    }
}
