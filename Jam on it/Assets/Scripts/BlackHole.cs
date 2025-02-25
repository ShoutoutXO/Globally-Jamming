using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    public float pullForce = 50f;  // Base pull force
    public float pullRadius = 20f; // Max pull range
    public Vector3 teleportPosition; // Teleport location

    private void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);

                if (distance < pullRadius) // Only pull if inside the range
                {
                    Vector2 direction = (transform.position - player.transform.position).normalized;

                    // Pull force gets stronger as the player gets closer
                    float forceAmount = Mathf.Lerp(pullForce * 0.1f, pullForce, 1 - (distance / pullRadius));

                    // Apply force directly to velocity for an instant pull effect
                    playerRb.linearVelocity += direction * forceAmount * Time.fixedDeltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered teleport to: " + teleportPosition);
            other.transform.position = teleportPosition; // Teleport when touching
        }
    }
}