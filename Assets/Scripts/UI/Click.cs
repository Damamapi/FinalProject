using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Click : MonoBehaviour
{

    [SerializeField] Animator ClickToMove;
    public CanvasGroup clickText;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FadeText();
        }
    }

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(3);
        clickText.alpha = 0f;
        clickText.gameObject.SetActive(true);
        clickText.DOFade(1, 1f).SetEase(Ease.InCubic);
    }

    private void FadeText()
    {
        clickText.gameObject.SetActive(true);
        clickText.DOFade(0, 1f).SetEase(Ease.InCubic).OnComplete( () =>
        {
            gameObject.SetActive(false);
        });
    }
}


