using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class RotateLevel : MonoBehaviour
{
    public float rotationDuration = 0.5f;
    private int orientationIndex = 2;
    private float[] orientations = { 0f, 90f, 180f, 270f };

    public static bool isRotating = false;

    public CinemachineVirtualCamera cam;

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateVertical();
        }
    }

    void RotateY(int direction)
    {
        isRotating = true;
        orientationIndex = (orientationIndex + direction + orientations.Length) % orientations.Length;
        float targetRotationY = orientations[orientationIndex];
        Vector3 targetRotation = new Vector3(0, targetRotationY, transform.eulerAngles.z);
        transform.DORotate(targetRotation, rotationDuration, RotateMode.Fast)
                 .SetEase(Ease.OutCirc)
                 .OnComplete(() => { isRotating = false; });
    }

    void RotateVertical()
    {
        isRotating = true;

        Sequence s = DOTween.Sequence();

        Vector3 levelTargetRotation = transform.eulerAngles + new Vector3(0, 0, 180f);
        Tweener levelRotateTween = transform.DORotate(levelTargetRotation, rotationDuration, RotateMode.FastBeyond360)
                                            .SetEase(Ease.OutCirc);

        float cameraTargetRotationX = cam.transform.eulerAngles.x > 180f ?
                                      cam.transform.eulerAngles.x - 360f :
                                      cam.transform.eulerAngles.x;
        cameraTargetRotationX = cameraTargetRotationX * -1f;
        Vector3 cameraTargetRotation = new Vector3(cameraTargetRotationX, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        Tweener cameraRotateTween = cam.transform.DORotate(cameraTargetRotation, rotationDuration)
                                              .SetEase(Ease.OutCirc);

        s.Append(levelRotateTween);
        s.Join(cameraRotateTween);

        s.OnComplete(() => { isRotating = false; });
    }
}


