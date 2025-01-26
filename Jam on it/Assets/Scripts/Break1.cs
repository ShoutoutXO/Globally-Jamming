using UnityEngine;

public class Break1 : MonoBehaviour
{
    public GameObject Bubble; // Reference to the trampoline object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the trigger
        if (collision.CompareTag("Player")) // Fixed missing parenthesis
        {
            Destroy(Bubble); // Correctly destroy the referenced object
        }
    }
}