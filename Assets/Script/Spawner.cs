using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] GameObject[] Enemies;
    [SerializeField] GameObject[] Spawns;
    private float spawnInterval = 3f;
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            EnemSpawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void EnemSpawn()
    {
        if (Enemies.Length == 0 || Spawns.Length == 0)
        {
            Debug.LogWarning("Spawner arrays are empty! Please assign Enemies and Spawns in the Inspector.");
            return;
        }

        int randomEnemyIndex = Random.Range(0, Enemies.Length);
        GameObject CurrentEnem = Enemies[randomEnemyIndex];

        int randomSpawnIndex = Random.Range(0, Spawns.Length);
        GameObject CurrentSpawn = Spawns[randomSpawnIndex];

        if (CurrentSpawn != null && CurrentEnem != null)
        {
            Instantiate(CurrentEnem, CurrentSpawn.transform.position, Quaternion.identity);
            Debug.Log($"Spawned {CurrentEnem.name} at position: {CurrentSpawn.name}");
        }
    }
}
