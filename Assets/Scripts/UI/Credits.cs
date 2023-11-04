using UnityEngine;
using DG.Tweening;

public class Credits : MonoBehaviour
{
    public float scrollDuration = 10f;
    private RectTransform creditsPanel;
    private Vector2 initialPosition;
    private Vector2 finalPosition = new Vector2(0, 3298.595f);

    private void Awake()
    {
        creditsPanel = GetComponent<RectTransform>();
        initialPosition = creditsPanel.anchoredPosition;
    }

    private void OnEnable()
    {
        creditsPanel.anchoredPosition = initialPosition;
        StartScrolling();
    }

    private void OnDisable()
    {
        creditsPanel.anchoredPosition = initialPosition;
        creditsPanel.DOKill();
    }

    private void StartScrolling()
    {
        creditsPanel.DOAnchorPosY(finalPosition.y, scrollDuration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}