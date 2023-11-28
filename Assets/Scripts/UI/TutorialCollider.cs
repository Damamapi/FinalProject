using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCollider : MonoBehaviour
{
    public CanvasGroup textRotate;
    public Transform player;

    private InputHandler inputHandler;

    bool activated = false;
    bool loadingScene = true;
    bool world1 = false;

    private void Start()
    {
        world1 = SceneManager.GetActiveScene().name.Contains("World1");
        StartCoroutine(FinishLoading());
        inputHandler = FindObjectOfType<GameManager>().inputHandler;
    }

    public void Update()
    {
        if (loadingScene) return;
        if (!activated && Vector3.Distance(player.position, transform.position) < 1)
        {
            ActivateText();
        }
        
        if (inputHandler.HasReceivedRotateLeftInput() || inputHandler.HasReceivedRotateRightInput())
        {
            if (world1) DeactivateText();
        }

        if (inputHandler.HasReceivedFlipInput() && !world1)
        {
            DeactivateText();
        }

    }

    IEnumerator FinishLoading()
    {
        yield return new WaitForSeconds(2.5f);
        loadingScene = false;
    }
 
    private void ActivateText()
    {
        activated = true;
        textRotate.alpha = 0f;
        textRotate.gameObject.SetActive(true);
        textRotate.DOFade(1, 1f).SetEase(Ease.InCubic);
      
    }

    private void DeactivateText()
    {
        textRotate.alpha = 1f;
        textRotate.DOFade(0, 1f).SetEase(Ease.InCubic).OnComplete( () =>
        {
            textRotate.gameObject.SetActive(false);
        });

    }




}