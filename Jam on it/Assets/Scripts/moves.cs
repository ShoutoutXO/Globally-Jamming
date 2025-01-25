using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moves : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Find the player in the scene by tag (assuming the player has the tag "Player")
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player object has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player's position
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}