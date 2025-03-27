using UnityEngine;
using System.Collections; // Required for coroutines

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject itemToEnable; // SerializeField makes it visible in the Inspector but keeps it private

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && itemToEnable != null)
        {
            StartCoroutine(EnableItemWithDelay()); // Uses a coroutine for WebGL physics update issues
        }
    }

    private IEnumerator EnableItemWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure proper physics update
        itemToEnable.SetActive(true);

        // Enable collider if it exists
        if (itemToEnable.TryGetComponent(out Collider2D itemCollider))
        {
            itemCollider.enabled = true;
        }
    }
}