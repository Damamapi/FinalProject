using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Click : MonoBehaviour
{

    [SerializeField] Animator ClickToMove;


    public GameObject DesactiveArrow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DesactiveArrow.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(240f);
        
        
    }
}


