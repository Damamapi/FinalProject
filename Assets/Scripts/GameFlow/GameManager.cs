using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : MonoBehaviour
{
    public bool isMobile;

    public InputHandler inputHandler;

    // Touch Controls
    private SwipeDetection swipeDetection;
    private InputManager inputManager;

    public void Awake()
    {
        
        // isMobile = true;
        // Up for desktop testing, down for building
        isMobile = Application.isMobilePlatform;

        inputHandler = new InputHandler();
        inputManager = GetComponent<InputManager>();
        swipeDetection = GetComponent<SwipeDetection>();

        if (isMobile) inputHandler.Init(swipeDetection);

        else
        {
            inputManager.enabled = false;
            swipeDetection.enabled = false;
            inputHandler.Init();
        }
    }
}
