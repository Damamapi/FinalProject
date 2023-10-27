using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credits");
    }
}
