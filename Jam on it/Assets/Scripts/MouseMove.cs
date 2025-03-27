using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float speed = 10f; // Speed at which the character moves
    public float boundaryX = 8f; // Horizontal boundary for movement
    private Rigidbody2D rb;
    private float lastMouseX;

    // Flag to disable movement during knockback
    public bool isKnockedBack = false;
    private float knockbackEndTime = 0f; // Track when knockback should end

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        lastMouseX = Input.mousePosition.x; // Store initial mouse X position
    }

    void Update()
    {
        // If knockback time has passed, restore movement
        if (isKnockedBack && Time.time >= knockbackEndTime)
        {
            isKnockedBack = false;
            rb.linearVelocity = Vector2.zero; // Stop any remaining movement
        }

        // If still knocked back, prevent movement
        if (isKnockedBack)
            return;

        // MOVEMENT LOGIC
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Constrain movement to the X axis within the boundary
        float targetX = Mathf.Clamp(worldPosition.x, -boundaryX, boundaryX);

        // Smooth movement with velocity
        rb.linearVelocity = new Vector2((targetX - transform.position.x) * speed, rb.linearVelocity.y);

        // Determine the mouse movement direction
        float mouseDelta = mousePosition.x - lastMouseX;
        lastMouseX = mousePosition.x;

        // Flip the character instantly
        if (mouseDelta < 0) // Moving left
        {
            transform.eulerAngles = new Vector3(0, 167.534f, 0);
        }
        else if (mouseDelta > 0) // Moving right
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Call this function to start knockback
    public void ApplyKnockback(float duration)
    {
        isKnockedBack = true;
        knockbackEndTime = Time.time + duration;
    }
}