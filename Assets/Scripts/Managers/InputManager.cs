using UnityEngine;

public static class InputManager
{
    public static float horizontal = 0.0f;
    public static float vertical = 0.0f;

    public static bool isLMBDown = false;
    public static bool isRMB = false;
    public static bool isSpace = false;
    public static bool isEsc = false;

    public static void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isLMBDown = Input.GetMouseButtonDown(0);
        isRMB = Input.GetMouseButton(1);
        isSpace = Input.GetKeyDown(KeyCode.Space);
        isEsc = Input.GetKeyDown(KeyCode.Escape);
    }
}