using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIChaseNShoot : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public float chaseRange = 10f; // Line of sight range
    public float fireRange = 8f; // Range within which the AI can shoot
    public float timeBetweenShots = 2f; // Time between AI shots
    public GameObject bulletPrefab; // Bullet prefab
    public Transform firePoint; // Bullet firing point

    private Transform player; // Reference to the player
    private bool canShoot = true; // Shooting cooldown
    private bool isFacingRight = true; // Track the facing direction

    void Start()
    {
        // Find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within the chase range
            if (distanceToPlayer <= chaseRange)
            {
                // Move toward the player
                ChasePlayer();

                // Check if the player is within firing range
                if (distanceToPlayer <= fireRange)
                {
                    if (canShoot)
                    {
                        StartCoroutine(ShootAtPlayer());
                    }
                }
            }
        }
    }

    private void ChasePlayer()
    {
        // Determine the direction toward the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Flip the AI if necessary
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }

        // Move toward the player
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Flip()
    {
        // Flip the AI sprite
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator ShootAtPlayer()
    {
        canShoot = false;

        // Instantiate a bullet at the firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Wait for the cooldown time
        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize chase and fire ranges
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

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