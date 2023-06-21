using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] spawnSlots;
    [SerializeField] float spawnRate;
    [SerializeField] float spawnCounter;

    private void Start()
    {
        SpawnEnemy();
    }
    private void Update()
    {
        spawnCounter += Time.deltaTime;

        if (spawnCounter > 1 / spawnRate)
        {
            SpawnEnemy();
        }
    }

    [ContextMenu("SpawnEnemy")]
    private void SpawnEnemy()
    {
        spawnCounter = 0;
        int randomSlot = Random.Range(0, spawnSlots.Length);
        BoxCollider2D slotCol = spawnSlots[randomSlot].GetComponent<BoxCollider2D>();
        BoxCollider2D enemyCol = enemyPrefab.GetComponent<BoxCollider2D>();
        float randomXPos = Random.Range(slotCol.bounds.min.x + enemyCol.size.x / 2, slotCol.bounds.max.x - enemyCol.size.x / 2);
        float randomYPos = Random.Range(slotCol.bounds.min.y, slotCol.bounds.max.y);

        Vector3 randomSpawnPos = new Vector3(randomXPos, randomYPos, 0);

        
        GameObject enemySpawned = Instantiate(enemyPrefab, spawnSlots[randomSlot].transform);
        enemySpawned.GetComponent<Enemy>().SetPosition(randomSpawnPos);
    }
}
