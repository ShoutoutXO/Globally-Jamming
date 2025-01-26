using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{

    public float rotationSpeed = 100f; // Speed of rotation in degrees per second
    private void Update()
    {
        // Rotate the object around its Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

}