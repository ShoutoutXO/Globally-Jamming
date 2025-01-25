using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet speed
    public float lifetime = 5f; // Bullet lifetime in seconds

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Apply velocity to the bullet
        rb.linearVelocity = transform.right * speed; // Assuming the bullet moves to the right of its local space

        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}