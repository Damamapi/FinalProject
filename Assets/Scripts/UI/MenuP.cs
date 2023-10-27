using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuP : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject PauseButton;
    private bool GamePaused = false;
    public string restarLevel;

    private void Update()
    {
        if (InputControl.IsInputAllowed && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused) Resume();
            else PauseUI();

        }
    }


    public void PauseUI()
    {
        GamePaused = true;
        pauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0f;


    }
    public void Resume()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        pauseMenu.SetActive(false);

    }
    public void Restart()
    {
        GamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(restarLevel);

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
