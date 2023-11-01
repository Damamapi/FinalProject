using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Click : MonoBehaviour
{

    [SerializeField] Animator ClickToMove;
    public CanvasGroup clickText;

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

        Debug.Log("Start Coroutine");
        yield return new WaitForSeconds(3);
        clickText.alpha = 0f;
        clickText.gameObject.SetActive(true);
        clickText.DOFade(1, 1f).SetEase(Ease.InCubic);
        Debug.Log("End Coroutine");



    }
}


