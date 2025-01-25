using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMovin : MonoBehaviour
{
    public Transform pointA; // Reference to point A (assigned in the Inspector)
    public Transform pointB; // Reference to point B (assigned in the Inspector)
    public float speed = 5f; // Movement speed
    private Transform currentPoint; // Target point
    private Rigidbody2D rb; // Rigidbody2D component
    private bool isFacingRight = true; // Track the facing direction of the sprite
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize Rigidbody2D
        currentPoint = pointB; // Start moving toward point B
    }

    void Update()
    {
        // Calculate direction to the current point
        Vector2 direction = (currentPoint.position - transform.position).normalized;

        // Apply linear velocity to move toward the current point
        rb.linearVelocity = direction * speed;

        // Check distance to the current point and switch targets if necessary
        if (Vector2.Distance(transform.position, currentPoint.position) <= 0.5f)
        {
            // Toggle between pointA and pointB
            currentPoint = currentPoint == pointB ? pointA : pointB;

            // Flip sprite direction if necessary
            FlipSprite(direction.x);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the trigger
        if (collision.CompareTag("Player"))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FlipSprite(float xDirection)
    {
        // Flip the sprite based on the X direction
        if ((xDirection > 0 && !isFacingRight) || (xDirection < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Flip the sprite
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the points and the path in the Scene view
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointA.position, 0.5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pointB.position, 0.5f);
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}