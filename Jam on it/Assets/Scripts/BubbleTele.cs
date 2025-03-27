using System.Collections; // Make sure this is included!
using UnityEngine;

public class BulletTeleport : MonoBehaviour
{
    public Vector3 targetSize = new Vector3(2f, 2f, 2f); // Final size of the bubble
    public float growSpeed = 1f; // Growth speed
    public float bulletSpeed = 5f; // Speed after shooting
    public float delayBeforeShoot = 1f; // Delay before shooting
    public Vector3 teleportPosition; // Where the player will be teleported

    private Transform player;
    private bool isGrowing = true;
    private bool isShot = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        transform.localScale = Vector3.zero; // Start tiny
        StartCoroutine(GrowBubble());

        if (teleportPosition == Vector3.zero) // If it's not set in the Inspector, default it
        {
            teleportPosition = new Vector3(0, 0, 0); // Adjust to your desired teleport spot
        }

    }

    private IEnumerator GrowBubble()
    {
        // Grow until target size is reached
        while (transform.localScale.x < targetSize.x)
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
            yield return null;
        }

        // Wait a bit before shooting
        yield return new WaitForSeconds(delayBeforeShoot);
        isGrowing = false;

        // Shoot toward the player
        if (player != null)
        {
            isShot = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport the player when hit
            other.transform.position = teleportPosition;
            Destroy(gameObject); // Destroy the bubble after teleporting the player
        }
    }
}