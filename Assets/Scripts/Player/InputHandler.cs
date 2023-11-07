using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{

    private TouchControls touchControls;
    private InputAction clickAction;
    private InputAction rotateLeftAction;
    private InputAction rotateRightAction;
    private InputAction flipAction;

    private bool isMobile = false;

    // flags for mobile rotation
    bool rotateLeft, rotateRight, flip, tap = false;

    // For desktop
    public void Init()
    {
        touchControls = new TouchControls();
        touchControls.Desktop.Enable();
        clickAction = touchControls.Desktop.Click;
        rotateLeftAction = touchControls.Desktop.RotateLeft;
        rotateRightAction = touchControls.Desktop.RotateRight;
        flipAction = touchControls.Desktop.Flip;
    }

    // For mobile
    public void Init(SwipeDetection swipeDetection)
    {
        isMobile = true;
        touchControls = new TouchControls();
        touchControls.Touch.Enable();
        swipeDetection.OnSwipe += HandleSwipe;
        swipeDetection.OnTap += HandleTap;
    }

    public void ResetFlags()
    {
        rotateLeft = rotateRight = flip = tap = false;
    }

    public bool HasReceivedClickInput()
    {
        if (InputControl.IsInputAllowed)
        {
            if (isMobile) return tap;
            else return clickAction.triggered;
        }
        return false;
    }

    public Ray GetRay()
    {
        Vector2 position = Application.isMobilePlatform
            ? Touchscreen.current.primaryTouch.position.ReadValue()
            : Mouse.current.position.ReadValue();
        return Camera.main.ScreenPointToRay(position);
    }

    public bool HasReceivedRotateLeftInput()
    {
        if (InputControl.IsInputAllowed)
        {
            if (isMobile) return rotateLeft;
            else return rotateLeftAction.triggered;
        }
        return false;
    }

    public bool HasReceivedRotateRightInput()
    {
        if (InputControl.IsInputAllowed) 
        {
            if (isMobile) return rotateRight;
            else return rotateRightAction.triggered;
        }
        return false;
    }

    public bool HasReceivedFlipInput()
    {
        if (InputControl.IsInputAllowed) 
        {
            if (isMobile) return flip;
            else return flipAction.triggered;
        }
        return false;
    }

    private void HandleSwipe(SwipeDetection.SwipeDir direction)
    {
        switch (direction)
        {
            case SwipeDetection.SwipeDir.Left:
                rotateLeft = true;
                break;
            case SwipeDetection.SwipeDir.Right:
                rotateRight = true;
                break;
            case SwipeDetection.SwipeDir.Up:
                flip = true;
                break;
            case SwipeDetection.SwipeDir.Down:
                flip = true;
                break;
        }
    }

    private void HandleTap()
    {
        tap = true;
    }
}
