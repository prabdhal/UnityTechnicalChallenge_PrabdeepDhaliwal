using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    [SerializeField] private float spawnInterval = 5f; // Interval between spawns
    private float spawnTimer;
    #endregion

    #region Init & Update
    private void Start()
    {
        spawnTimer = spawnInterval;
        SpawnEnemies();
    }

    private void Update()
    {
        // Countdown for spawning
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemies();
            spawnTimer = spawnInterval;
        }
    }
    #endregion

    #region Spawn Logic
    private void SpawnEnemies()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.SpawnEnemy();
            }
        }
    }
    private void DespawnEnemies()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.DespawnEnemy();
            }
        }
    }
    #endregion
}
