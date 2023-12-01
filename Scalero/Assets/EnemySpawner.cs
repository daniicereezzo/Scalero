using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<Transform> spawnPoints;
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    public int maxEnemies = 5;
    RaycastHit2D cast;
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
            yield return new WaitForSeconds(spawnRate);
            SpawnEnemy();
        }

        // Check if there are any spawn points available
        
    }
    void SpawnEnemy()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies)
        {
            return;
        }
        bool validSpawnPoint = false;
        int iterations = 0;
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return;
        }

        // Select a random spawn point index
        int randomIndex = Random.Range(0, spawnPoints.Count);
        while (!validSpawnPoint)
        {
            cast = Physics2D.CircleCast(spawnPoints[randomIndex].position, 0.3f, Vector2.zero);
            if (cast.collider == null)
            {
                validSpawnPoint = true;
            }
            else
            {
                randomIndex = Random.Range(0, spawnPoints.Count);
                iterations++;
                Debug.Log("enemy at " + spawnPoints[randomIndex].position);
                
                if(iterations > spawnPoints.Count)
                {
                    Debug.LogWarning("No spawn points available.");
                    return;
                }
            }                                 
        }

        // Get the random spawn point
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate enemy at the spawn point position
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
