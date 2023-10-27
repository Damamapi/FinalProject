using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public CanvasGroup mainMenu;
    public CanvasGroup settingsGroup;
    public void Play()
    {
        SceneManager.LoadScene("Level 0");
    }

    public void Settings()
    {
        mainMenu.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        settingsGroup.gameObject.SetActive(false);
    }

    public void Credit()
    {
        //SceneManager.LoadScene("Credits");
    }
}
