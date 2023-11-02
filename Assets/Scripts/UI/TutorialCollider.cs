using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public CanvasGroup textRotate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            textRotate.alpha = 1f;
            textRotate.gameObject.SetActive(true);
            textRotate.DOFade(1, 1f).SetEase(Ease.InCubic);
        }
   

    }
}
