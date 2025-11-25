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
    public float counttime;
   void Update()
    {
        counttime += Time.deltaTime;
        if (counttime >= waves[waveNumber].spawnTimer)
        {
            counttime = 0;
            SpawnEnemy();
        }
        if (waveNumber >= waves.Count)
        {
            waveNumber = 0;
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(waves[waveNumber].enemyPrefab, transform.position, transform.rotation);
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].enemiesPerWave)
        {
            waves[waveNumber].spawnedEnemyCount = 0;
            waveNumber++;
        }
    }
}
