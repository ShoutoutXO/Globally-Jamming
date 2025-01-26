using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float pullForce = 10f; // Strength of the pull
    private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D

    void FixedUpdate()
    {
        if (playerRb != null)
        {
            // Calculate direction and distance
            Vector2 direction = (transform.position - playerRb.transform.position).normalized;
            float distance = Vector2.Distance(transform.position, playerRb.transform.position);

            // Apply force based on distance
            float distanceFactor = Mathf.Clamp(1 / distance, 0.1f, 5f);
            float scaledForce = pullForce * distanceFactor * Time.fixedDeltaTime;

            Debug.Log("Applying force: " + direction * scaledForce);

            // Apply the pulling force
            playerRb.AddForce(direction * scaledForce, ForceMode2D.Force);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = collision.GetComponent<Rigidbody2D>();
            Debug.Log("Player detected: " + playerRb.name);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb = null;
            Debug.Log("Player left the black hole's range.");
        }
    }
}