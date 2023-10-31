using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string levelMusic;
    void Start()
    {
        AudioManager.instance.PlayMusic(levelMusic);       
    }
}
