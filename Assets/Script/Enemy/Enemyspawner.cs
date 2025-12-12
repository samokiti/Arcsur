using UnityEngine;
using System.Collections.Generic;

public class Enemyspawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int enemiesPerWave;
        public int spawnedEnemyCount;
    }

    public List<Wave> waves;
    public int waveNumber;

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnDistance = 10f;
    [SerializeField] private float maxSpawnDistance = 15f;

    void Update()
    {
        if (waves.Count == 0) return;

        waves[waveNumber].spawnTimer += Time.deltaTime;

        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(waves[waveNumber].enemyPrefab, RandomSpawnPoint(), transform.rotation);

        waves[waveNumber].spawnedEnemyCount++;

        if (waves[waveNumber].spawnedEnemyCount >= waves[waveNumber].enemiesPerWave)
        {
            waves[waveNumber].spawnInterval -= 0.17f;
            waves[waveNumber].spawnedEnemyCount = 0;
            if (waves[waveNumber].spawnInterval < 0.1f)
            {
                waves[waveNumber].spawnInterval = 0.1f;
            }
            waveNumber++;

            if (waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }
    private Vector2 RandomSpawnPoint()
    {
        Vector2 playerPos = Controller.Instance.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPos = playerPos + (randomDirection * randomDistance);

        return spawnPos;
    }
}