using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab; // Prefab từ Assets
        public GameObject enemyInstanceInScene; // Enemy có sẵn trong scene

        [HideInInspector] public Vector3 spawnPosition;
        [HideInInspector] public Quaternion spawnRotation;
        [HideInInspector] public GameObject currentInstance;
    }

    public List<EnemyData> enemies = new List<EnemyData>(10);
    public float respawnDelay = 25f;

    private bool isRespawning = false;

    private void Start()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.enemyInstanceInScene != null)
            {
                enemy.spawnPosition = enemy.enemyInstanceInScene.transform.position;
                enemy.spawnRotation = enemy.enemyInstanceInScene.transform.rotation;
                enemy.currentInstance = enemy.enemyInstanceInScene;
            }
        }
    }

    private void Update()
    {
        if (isRespawning) return;

        bool allDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy.currentInstance != null)
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            StartCoroutine(RespawnEnemies());
        }
    }

    private IEnumerator RespawnEnemies()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);

        foreach (var enemy in enemies)
        {
            if (enemy.currentInstance == null && enemy.enemyPrefab != null)
            {
                enemy.currentInstance = Instantiate(enemy.enemyPrefab, enemy.spawnPosition, enemy.spawnRotation);
            }
        }

        isRespawning = false;
    }
}
