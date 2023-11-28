using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0, 1)]
    private float directionTreshold = .9f;

    private InputManager inputManager;

    public enum SwipeDir
    {
        Left,
        Right,
        Up,
        Down
    }

    public event Action OnTap;
    public event Action<SwipeDir> OnSwipe;

    Vector2 startPosition;
    float startTime;
    Vector2 endPosition;
    float endTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        float distance = Vector2.Distance(startPosition, endPosition);
        float duration = endTime - startTime;

        if (distance >= minimumDistance && duration <= maximumTime)
        {
            Vector3 d = endPosition - startPosition;
            Vector2 direction = new Vector2(d.x, d.y).normalized;
            SwipeDirection(direction);
        }
        else if (distance < minimumDistance && duration < maximumTime)
        {
            OnTap?.Invoke();
        }

    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionTreshold)
        {
            OnSwipe?.Invoke(SwipeDir.Up);
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionTreshold)
        {
            OnSwipe?.Invoke(SwipeDir.Down);
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionTreshold)
        {
            OnSwipe?.Invoke(SwipeDir.Left);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionTreshold)
        {
            OnSwipe?.Invoke(SwipeDir.Right);
        }
    }
}
