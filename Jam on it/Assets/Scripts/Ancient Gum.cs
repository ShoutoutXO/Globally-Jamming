using UnityEngine;
using UnityEngine.SceneManagement;

public class AncientGum : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the trigger
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("End");
        }
    }
}
