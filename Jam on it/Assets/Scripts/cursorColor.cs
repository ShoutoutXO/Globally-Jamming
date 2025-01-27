using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorColor : MonoBehaviour
{

    public Texture2D arrowCursor;
    public Texture2D electricCursor;

    // Changes the color of the Cursor on screen 
    void Start()
    {
        Cursor.SetCursor(arrowCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Changes the color a second time on trigger with something
    void OnMouseEnter()
    {
        Cursor.SetCursor(electricCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
