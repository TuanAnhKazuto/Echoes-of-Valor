using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;        // Số lượng quái trong wave này
        public float spawnDelay = 1f; // Delay giữa mỗi lần spawn quái
    }

    public BaseCharacter player;
    public List<GameObject> enemyPrefabs;  // Danh sách prefab quái
    public List<Wave> waves;               // Danh sách wave
    public Transform[] spawnPoints;        // Các điểm spawn có thể

    public float waveDelay = 5f;           // Delay giữa các wave

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;

        while (currentWaveIndex < waves.Count)
        {
            Wave wave = waves[currentWaveIndex];
            Debug.Log("Bắt đầu wave " + (currentWaveIndex + 1));

            for (int i = 0; i < wave.enemyCount; i++)
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(wave.spawnDelay);
            }

            currentWaveIndex++;

            if (currentWaveIndex < waves.Count)
            {
                Debug.Log("Chờ " + waveDelay + "s trước khi bắt đầu wave tiếp theo...");
                yield return new WaitForSeconds(waveDelay);
            }
        }

        Debug.Log("Tất cả các wave đã spawn!");
        isSpawning = false;
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnPoints.Length == 0)
            return;

        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
        newEnemy.GetComponent<BaseCharacter>().target = player;
    }
}
