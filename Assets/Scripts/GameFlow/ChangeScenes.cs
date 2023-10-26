using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public static void ChangeScene()
    {
        SceneManager.LoadScene("Level 2");
    }
}