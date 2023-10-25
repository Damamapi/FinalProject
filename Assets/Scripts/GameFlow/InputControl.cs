using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public static bool IsInputAllowed { get; private set; } = false;

    public static void AllowInput()
    {
        IsInputAllowed = true;
    }
    public static void DisableInput()
    {
        IsInputAllowed = false;
    }
}
