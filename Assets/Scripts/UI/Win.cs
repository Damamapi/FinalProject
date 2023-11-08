using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public string currentLevel;
    public string nextLevel;
    public void NextLevel()
    {
        AudioManager.Instance.PlaySFX("uiSelect");
        SceneManager.LoadScene(nextLevel);
    }

    public void Restart()
    {
        AudioManager.Instance.PlaySFX("uiSelect");
        SceneManager.LoadScene(currentLevel);

    }


}
