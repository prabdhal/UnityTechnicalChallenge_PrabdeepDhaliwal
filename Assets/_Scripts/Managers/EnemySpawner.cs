using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5f, 0f, 5f); 
    [SerializeField] private int maxEnemies = 5; 
    [SerializeField] private float spawnHeightOffset = 0.5f;
    [SerializeField] private float spawnInterval = 5f; // Timer for spawning
    [SerializeField]
    private LootDrop lootDrop;

    private List<BaseEnemyController> currentEnemies = new List<BaseEnemyController>();
    private float spawnTimer;
    #endregion

    #region Init & Update
    private void Start()
    {
        SpawnEnemy();
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        if (currentEnemies.Count < maxEnemies)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval; // Reset the timer
            }
        }
    }
    #endregion

    #region Spawn Logic
    public void SpawnEnemy()
    {
        if (currentEnemies.Count >= maxEnemies) return;

        Vector3 spawnPosition = GetRandomPositionWithinArea();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController controller = enemy.GetComponentInChildren<EnemyController>();
        controller.OnEnemyKilled_EnemyController += HandleEnemyDestroyed;
        if (lootDrop != null)
        {
            controller.OnEnemyKilled_EnemyController += lootDrop.DropLoot;
        }

        enemy.transform.parent = transform;

        currentEnemies.Add(controller);
    }

    public void DespawnAllEnemies()
    {
        foreach (var enemy in currentEnemies)
        {
            enemy.Kill();
        }
        currentEnemies.Clear();
    }

    private Vector3 GetRandomPositionWithinArea()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            spawnHeightOffset,
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        return transform.position + randomPosition;
    }
    #endregion

    #region Enemy Tracking
    private void HandleEnemyDestroyed(BaseEnemyController enemy)
    {
        currentEnemies.Remove(enemy);
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.up * spawnHeightOffset, spawnAreaSize);
    }
    #endregion
}
