using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BubbleGunMa : MonoBehaviour
{
    public float fireRange = 8f; // Shooting range
    public float timeBetweenShots = 2f; // Cooldown between shots
    public float bulletChargeTime = 1f; // Time to charge before shooting
    public float bulletSpeed = 5f; // Bullet speed
    public GameObject bulletPrefab; // Bullet prefab
    public Transform firePoint; // Fire point for bullets

    private Transform player; // Reference to the player
    private bool canShoot = true; // Control for shooting

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Validate references
        if (player == null) Debug.LogError("Player object not found! Ensure it has the 'Player' tag.");
        if (bulletPrefab == null) Debug.LogError("Bullet prefab not assigned!");
        if (firePoint == null) Debug.LogError("Fire point not assigned!");
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Shoot only if within fireRange
        if (distanceToPlayer <= fireRange && canShoot)
        {
            StartCoroutine(SpawnAndShoot());
        }
    }

    private IEnumerator SpawnAndShoot()
    {
        canShoot = false; // Prevent immediate re-shooting

        // Validate FirePoint and BulletPrefab
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogError("FirePoint or BulletPrefab is null! Check the Inspector.");
            yield break;
        }

        // Spawn and charge the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(bulletChargeTime);

        // Add velocity to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null && player != null)
        {
            Vector2 fireDirection = (player.position - bullet.transform.position).normalized;
            rb.linearVelocity = fireDirection * bulletSpeed; // Set bullet velocity
        }

        // Wait before the next shot
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw fireRange in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the trigger
        if (collision.CompareTag("Player"))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}