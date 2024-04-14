using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab of the enemy to spawn
    public float spawnInterval = 1f; // Time between enemy spawns
    public float spawnRadius = 5f; // The radius within which enemies can spawn
    public float heightOffset = 0.5f; // Height offset from the ground
    public bool isActive = true; // Whether the spawner is active or not

    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        // Loop infinitely while the spawner is active
        while (isActive)
        {
            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Calculate a random position within the spawn area
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = heightOffset; // Ensure enemies spawn on the same Y-level

            // Spawn an enemy at the random position
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    // This method can be called to stop spawning enemies
    public void StopSpawning()
    {
        StopCoroutine(SpawnEnemyRoutine());
    }
}
