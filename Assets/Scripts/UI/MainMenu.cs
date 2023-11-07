using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float fadeTime = 1.0f;

    public CanvasGroup mainMenu;
    public CanvasGroup settingsGroup;
    public CanvasGroup credits;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeIn(mainMenu));
    }

    public void Play()
    {
        AudioManager.Instance.PlaySFX("hihatOpen");
        SceneManager.LoadScene("World1 - 1");
    }

    IEnumerator FadeOutThenIn(CanvasGroup outGroup, CanvasGroup inGroup)
    {
        yield return FadeOut(outGroup);
        yield return FadeIn(inGroup);
    }

    IEnumerator FadeOut(CanvasGroup group)
    {
        group.alpha = 1f;
        Tween tween = group.DOFade(0, fadeTime).SetEase(Ease.OutCubic);
        yield return tween.WaitForCompletion();
        group.gameObject.SetActive(false);
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        group.alpha = 0f;
        group.gameObject.SetActive(true);
        Tween tween = group.DOFade(1, fadeTime).SetEase(Ease.InCubic);
        yield return tween.WaitForCompletion();
    }

    public void Settings()
    {
        AudioManager.Instance.PlaySFX("hihatOpen");
        StartCoroutine(FadeOutThenIn(mainMenu, settingsGroup));
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.PlaySFX("hihatClosed");
        StartCoroutine(FadeOutThenIn(settingsGroup, mainMenu));
    }

    public void BackToMainFromCredits()
    {
        AudioManager.Instance.PlaySFX("hihatClosed");
        StartCoroutine(FadeOutThenIn(credits, mainMenu));
    }

    public void Credits()
    {
        AudioManager.Instance.PlaySFX("hihatOpen");
        StartCoroutine(FadeOutThenIn(mainMenu, credits));
    }

    public void MuteMusic()
    {
        AudioManager.Instance.PlaySFX("hihatOpen");
        AudioManager.Instance.ToggleMusic();
    }

    public void MuteSFX()
    {
        AudioManager.Instance.ToggleSFX();
        AudioManager.Instance.PlaySFX("hihatOpen");
    }
}
