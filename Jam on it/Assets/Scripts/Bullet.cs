using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float knockbackForce = 10f;

    private Rigidbody2D rb;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing from Bullet!");
            return;
        }

        rb.linearVelocity = new Vector2(speed * direction, 0);
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(int dir)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D is missing from Bullet!");
                return;
            }
        }

        direction = dir;
        rb.linearVelocity = new Vector2(speed * direction, 0);

        // Rotate the bullet based on movement direction
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Flip the sprite if needed
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * direction;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            MouseMove mouseMove = collision.GetComponent<MouseMove>();

            if (playerRb != null)
            {
                Debug.Log("Applying knockback...");
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                Debug.Log("Knockback direction: " + knockbackDirection);

                playerRb.linearVelocity = new Vector2(knockbackDirection.x * knockbackForce, playerRb.linearVelocity.y);

                if (mouseMove != null)
                {
                    Debug.Log("Applying Knockback for 0.5s...");
                    StartCoroutine(ApplyKnockback(mouseMove));
                }
            }
            else
            {
                Debug.Log("No Rigidbody2D found on Player!");
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator ApplyKnockback(MouseMove mouseMove)
    {
        if (mouseMove == null) yield break;

        mouseMove.isKnockedBack = true;
        yield return new WaitForSeconds(0.5f);
        mouseMove.isKnockedBack = false;

        Rigidbody2D playerRb = mouseMove.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
        }
    }
}