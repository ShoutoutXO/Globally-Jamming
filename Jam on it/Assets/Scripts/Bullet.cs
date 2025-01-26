using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet speed
    public float lifetime = 5f; // Bullet lifetime in seconds

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply velocity to the bullet
            rb.linearVelocity = transform.right * speed; // Assuming the bullet moves to the right of its local space
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on the bullet object.");
        }

        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}