using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Click : MonoBehaviour
{

    [SerializeField] Animator ClickToMove;
    public CanvasGroup clickText;

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
        Invoke(nameof(FadeText), 3f);
    }

    private void FadeText()
    {
        clickText.DOFade(0, 1f).SetEase(Ease.InCubic).OnComplete( () =>
        {
            gameObject.SetActive(false);
        });
    }
}


