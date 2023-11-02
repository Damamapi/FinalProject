using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public CanvasGroup textRotate;
    public Transform player;

    public void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 1)
        {
            ActivateText();
            gameObject.SetActive(false);

        }

    }
 
    private void ActivateText()
    {
        textRotate.alpha = 0f;
        textRotate.gameObject.SetActive(true);
        textRotate.DOFade(1, 1f).SetEase(Ease.InCubic);
      
    }

   


}