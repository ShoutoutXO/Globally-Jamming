using UnityEngine;

public class DeathFromFall : MonoBehaviour
{
    public Vector3 teleportPosition; // The teleport location (set in Inspector)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            // Log for debugging
            Debug.Log("Player triggered teleport to: " + teleportPosition);

            // Teleport the player to the defined position
            other.transform.position = teleportPosition;
        }
    }
}