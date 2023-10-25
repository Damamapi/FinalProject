using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float rotationDuration = 0.5f;
    private int orientationIndex = 2;
    private float[] orientations = { 0f, 90f, 180f, 270f };

    public bool isRotating = false;

    void Update()
    {
        if (!InputControl.IsInputAllowed) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateY(-1);
            AudioManager.instance.PlaySound("rotateLeft");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RotateY(1);
            AudioManager.instance.PlaySound("rotateRight");
        }
    }

    void RotateY(int direction)
    {
        isRotating = true;
        orientationIndex = (orientationIndex + direction + orientations.Length) % orientations.Length;
        float targetRotationY = orientations[orientationIndex];
        Vector3 targetRotation = new Vector3(0, targetRotationY, 0);
        transform.DORotate(targetRotation, rotationDuration, RotateMode.Fast)
                 .SetEase(Ease.OutCirc)
                 .OnComplete(() => { isRotating = false; });
    }
}


