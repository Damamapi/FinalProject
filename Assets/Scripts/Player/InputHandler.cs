using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public bool HasReceivedClickInput()
    {
        return Input.GetMouseButtonDown(0) && InputControl.IsInputAllowed;
    }

    public Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    public bool HasReceivedRotateLeftInput()
    {
        return Input.GetKeyDown(KeyCode.Q) && InputControl.IsInputAllowed;
    }

    public bool HasReceivedRotateRightInput()
    {
        return Input.GetKeyDown(KeyCode.E) && InputControl.IsInputAllowed;
    }

    public bool HasReceivedFlipInput()
    {
        return Input.GetKeyDown(KeyCode.W) && InputControl.IsInputAllowed;
    }
}
