using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public string currentLevel;
    public string nextLevel;
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void Restart()
    {
       
        SceneManager.LoadScene(currentLevel);

    }


}
