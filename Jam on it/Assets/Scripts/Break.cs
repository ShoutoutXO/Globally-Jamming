using UnityEngine;
using System.Collections; // Ensure this namespace is included for IEnumerator

public class Break : MonoBehaviour
{
    public GameObject Bubble; // Reference to the trampoline object
    public float destructionDelay = 1f; // Delay before destruction

    private bool isBounced = false; // To ensure the trampoline is destroyed only once after bouncing

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the trigger
        if (collision.CompareTag("Player") && !isBounced)
        {
            isBounced = true;

            // Apply a bounce effect to the player
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Use linearVelocity instead of velocity (new Unity standard)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 10f); // Adjust the y-axis velocity for the bounce
            }

            // Start the destruction process
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destructionDelay);

        // Destroy the trampoline (Bubble)
        Destroy(Bubble);
    }
}