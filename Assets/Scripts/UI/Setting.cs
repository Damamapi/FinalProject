using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Basic Level");
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
