using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField]
    private float _windForce = 0f; // Declare the wind force variable correctly

    private void OnTriggerStay2D(Collider2D other) // Use Collider2D for 2D physics
    {
        if (other != null)
        {
            var rb = other.GetComponent<Rigidbody2D>(); // Use Rigidbody2D for 2D physics
            if (rb != null)
            {
                var dir = transform.up; // Direction of the wind
                rb.AddForce(dir * _windForce); // Apply the wind force
            }
        }
    }
}