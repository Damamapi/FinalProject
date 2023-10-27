using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float rotationTime = 1f;
    public float initialDelay = 3f;

    void Start()
    {
        StartCoroutine(StartRotation());
    }

    IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(initialDelay);
        RotateAndLoop();
    }

    void RotateAndLoop()
    {
        transform.DORotate(new Vector3(0, 540, 0), rotationTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Restart);
    }
}
