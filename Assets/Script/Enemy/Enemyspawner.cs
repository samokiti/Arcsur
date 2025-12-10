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
    void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime;
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpawnEnemy();
        }
        if (waves[waveNumber].spawnedEnemyCount >= waves[waveNumber].enemiesPerWave)
        {
            waves[waveNumber].spawnedEnemyCount = 0;
            if (waves[waveNumber].spawnInterval > 0.3f)
            {
                waves[waveNumber].spawnInterval *= 0.9f;
            }
            waveNumber++;
        }
        if (waveNumber >= waves.Count )
        {
            waveNumber = 0;
        }
    }

    private void SpawnEnemy()

    {
        Instantiate(waves[waveNumber].enemyPrefab, RandomSpawnPoint(), transform.rotation);
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].enemiesPerWave)
        {

            waves[waveNumber].spawnedEnemyCount = 0;
            waveNumber++;
        }
    }
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint; 
        
        spawnPoint.x = Random.Range(-5,5);
        spawnPoint.y = Random.Range(-5,5);

        return spawnPoint;
    }
}