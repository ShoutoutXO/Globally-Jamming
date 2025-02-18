using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float speed = 10f; // Speed at which the character moves
    public float boundaryX = 8f; // Horizontal boundary for movement
    private Rigidbody2D rb;
    private float lastMouseX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        lastMouseX = Input.mousePosition.x; // Store initial mouse X position
    }

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen space mouse position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Constrain movement to the X axis (left and right) within the boundary
        float targetX = Mathf.Clamp(worldPosition.x, -boundaryX, boundaryX);

        // Set the Rigidbody2D's velocity for smooth movement
        rb.linearVelocity = new Vector2((targetX - transform.position.x) * speed, rb.linearVelocity.y);

        // Determine the mouse movement direction
        float mouseDelta = mousePosition.x - lastMouseX;
        lastMouseX = mousePosition.x;

        // Flip the character instantly
        if (mouseDelta < 0) // Moving left
        {
            transform.eulerAngles = new Vector3(0, 167.534f, 0); // Set rotation instantly
        }
        else if (mouseDelta > 0) // Moving right
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // Set rotation instantly
        }
    }
}