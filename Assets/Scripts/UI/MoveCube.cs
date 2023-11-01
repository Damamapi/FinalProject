using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public GameObject DesactiveTutorialArrow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DesactiveTutorialArrow.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DesactiveTutorialArrow.SetActive(false);
        }
    }
}
