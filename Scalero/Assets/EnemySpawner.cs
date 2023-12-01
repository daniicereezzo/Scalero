using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<Transform> spawnPoints;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new List<Transform>();
        foreach(Transform child in transform)
        {
            spawnPoints.Add(child);
        }
        StartCoroutine(SpawnNewEnemies());
        
    }

    IEnumerator SpawnNewEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            SpawnEnemy();
        }

        // Check if there are any spawn points available
        
    }
    void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return;
        }

        // Select a random spawn point index
        int randomIndex = Random.Range(0, spawnPoints.Count);

        // Get the random spawn point
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate enemy at the spawn point position
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
