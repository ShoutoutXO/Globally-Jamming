using System.Collections;
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
    public Vector3 teleportPosition; // Position to teleport player to
    public Vector3 enemyTeleportPosition; // Teleport the enemy also

    private Transform player;
    private bool canShoot = true;
    private bool isFacingRight = true;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }

        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned in AIChaseNShoot!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within chase range
            if (distanceToPlayer <= chaseRange)
            {
                // Chase the player if within range
                ChasePlayer();

                // If within fire range, shoot at the player
                if (distanceToPlayer <= fireRange && canShoot)
                {
                    StartCoroutine(ShootAtPlayer());
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize chase range (line of sight range)
        Gizmos.color = Color.yellow; // Set color for chase range
        Gizmos.DrawWireSphere(transform.position, chaseRange); // Draw the chase range sphere

        // Visualize fire range (shooting range)
        Gizmos.color = Color.red; // Set color for fire range
        Gizmos.DrawWireSphere(transform.position, fireRange); // Draw the fire range sphere
    }

    private void ChasePlayer()
    {
        // Get direction toward the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move toward the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Correctly flip to face the player only when necessary
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the enemy sprite by inverting its scale
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private IEnumerator ShootAtPlayer()
    {
        canShoot = false;

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned!");
            yield break;
        }

        if (firePoint == null)
        {
            Debug.LogError("FirePoint is missing!");
            yield break;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript == null)
        {
            Debug.LogError("Bullet prefab does NOT have the Bullet script attached!");
        }
        else
        {
            bulletScript.SetDirection(isFacingRight ? 1 : -1);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportPosition;

            transform.position = enemyTeleportPosition;
        }
    }
}