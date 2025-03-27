using UnityEngine;

public class HideMouse : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false; // Hides the cursor
        Cursor.lockState = CursorLockMode.None; // Ensures the cursor can still move freely
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Cursor.visible = true; // Makes the cursor visible when tabbing out
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnApplicationQuit()
    {
        Cursor.visible = true; // Ensures the cursor is visible when quitting the game
    }
}