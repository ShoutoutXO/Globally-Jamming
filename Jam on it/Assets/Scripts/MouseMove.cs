using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float speed = 10f; // Speed at which the character moves
    public float boundaryX = 8f; // Horizontal boundary for movement
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen space mouse position to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Constrain movement to the X axis (left and right) within the boundary
        float targetX = Mathf.Clamp(worldPosition.x, -boundaryX, boundaryX);

        // Set the Rigidbody2D's linear velocity for smooth movement
        rb.linearVelocity = new Vector2((targetX - transform.position.x) * speed, rb.linearVelocity.y);
    }
}