using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerOnCollision : MonoBehaviour
{
    [System.Serializable]
    public class ItemSpawn
    {
        public GameObject itemToDestroy; // The item that will be destroyed
        public GameObject itemToSpawn;   // The item to spawn
        public GameObject spawnPoint;    // The empty GameObject where it should spawn
    }

    public ItemSpawn[] itemsToSpawn; // List of items with their spawn locations

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Change "Player" to your desired tag
        {
            foreach (ItemSpawn spawn in itemsToSpawn)
            {
                if (spawn.itemToDestroy != null)
                {
                    Destroy(spawn.itemToDestroy); // Destroy the assigned item
                }

                if (spawn.itemToSpawn != null && spawn.spawnPoint != null)
                {
                    // Spawn the new item at the exact position of the assigned empty GameObject
                    Instantiate(spawn.itemToSpawn, spawn.spawnPoint.transform.position, Quaternion.identity);
                }
            }
        }
    }
}