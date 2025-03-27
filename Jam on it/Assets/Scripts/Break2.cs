using UnityEngine;
using System.Collections;

public class Break2 : MonoBehaviour
{
    public GameObject Bubble; // Reference to the trampoline object
    public float destructionDelay = 1f; // Delay before destruction

    private bool isBounced = false; // Ensures trampoline is destroyed only once

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBounced)
        {
            isBounced = true;

            // Let the physics material handle the bounce (no need to manually apply force)

            // Start the destruction process
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destructionDelay);

        // Destroy the trampoline (Bubble)
        if (Bubble != null)
        {
            Destroy(Bubble);
        }
    }
}