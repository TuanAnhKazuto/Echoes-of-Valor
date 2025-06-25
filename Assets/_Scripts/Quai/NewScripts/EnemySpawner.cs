using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> enemies;      
    public List<Transform> spawnPoints;   
    public int spawnCount = 10;            
    public int timeSpawn = 15;            

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeSpawn);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemies.Count);
            int spawnPointIndex = i % spawnPoints.Count;

            Transform enemyPrefab = enemies[randomEnemyIndex];
            Transform spawnPoint = spawnPoints[spawnPointIndex];

            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(0.2f); 
        }
    }
}