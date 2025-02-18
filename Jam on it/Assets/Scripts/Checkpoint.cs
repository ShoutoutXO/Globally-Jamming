using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject itemToEnable; // Assign this manually in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that triggered the event has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // If the itemToEnable is assigned and not null
            if (itemToEnable != null)
            {
                // Enable the item in the Hierarchy
                itemToEnable.SetActive(true);
            }
      
        }
    }
}