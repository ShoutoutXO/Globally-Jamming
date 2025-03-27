using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BubbleGunMa : MonoBehaviour
{
    public float fireRange = 8f;
    public float timeBetweenShots = 2f;
    public float bulletChargeTime = 1f;
    public float bulletSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Vector3 teleportLocation = new Vector3(10, 5, 0); // Where the player gets teleported
    public float maxSize = 1.5f;
    public float growthSpeed = 1.5f;

    private Transform player;
    private bool canShoot = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null) Debug.LogError("Player object not found! Ensure it has the 'Player' tag.");
        if (bulletPrefab == null) Debug.LogError("Bullet prefab not assigned!");
        if (firePoint == null) Debug.LogError("Fire point not assigned!");
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= fireRange && canShoot)
        {
            StartCoroutine(SpawnAndShoot());
        }
    }

    private IEnumerator SpawnAndShoot()
    {
        canShoot = false;

        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogError("FirePoint or BulletPrefab is null! Check the Inspector.");
            yield break;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.localScale = Vector3.zero;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        float elapsedTime = 0f;
        while (elapsedTime < bulletChargeTime)
        {
            float scale = Mathf.Lerp(0.1f, maxSize, elapsedTime / bulletChargeTime);
            bullet.transform.localScale = new Vector3(scale, scale, 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bullet.transform.localScale = new Vector3(maxSize, maxSize, 1);

        if (rb != null && player != null)
        {
            Vector2 fireDirection = (player.position - bullet.transform.position).normalized;
            rb.linearVelocity = fireDirection * bulletSpeed;
        }

        BulletTeleport bulletTeleport = bullet.AddComponent<BulletTeleport>();
        bulletTeleport.teleportPosition = teleportLocation;

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = teleportLocation; // Teleport player if they touch the enemy
        }
    }
}