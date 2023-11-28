using UnityEngine;

public class InputControl : MonoBehaviour
{
    public static bool IsInputAllowed { get; private set; } = false;

    public static void EnableInput()
    {
        IsInputAllowed = true;
    }
    public static void DisableInput()
    {
        IsInputAllowed = false;
    }
}
