using System.Collections;
using UnityEngine;

public class BubbleTurret : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet to fire
    public Transform firePoint; // Where bullets spawn from
    public float fireRate = 2f; // Time between shots
    public float bulletSpeed = 10f;
    public Vector2 shootDirection = Vector2.right; // Default direction (right)

    void Start()
    {
        StartCoroutine(ShootAtDirection()); // Start shooting loop
    }

    IEnumerator ShootAtDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("BulletPrefab or FirePoint is not assigned!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = shootDirection.normalized * bulletSpeed; // Shoots in chosen direction
        }

        Destroy(bullet, 5f);
    }

    public void SetShootDirection(Vector2 newDirection)
    {
        shootDirection = newDirection.normalized; // Update direction dynamically
    }
}