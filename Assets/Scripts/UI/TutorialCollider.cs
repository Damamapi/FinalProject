using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public CanvasGroup textRotate;

    private void OnTriggerEnter(Collider  collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("TochCollider");
            textRotate.alpha = 0f;
            textRotate.gameObject.SetActive(true);
            textRotate.DOFade(1, 1f).SetEase(Ease.InCubic);

        }
   

    }
}
