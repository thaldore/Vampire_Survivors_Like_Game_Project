using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; // a list of groups of enemies to spawn in this wave
        public int waveQuota;   // the total number of enemies to spawn in this wave
        public float spawnInterval;     // the interval at which to spawn enemies
        public int spawnCount;  // the number of enemies already spawned in this wave

    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;  // The number of enemies to spawn in this wave
        public int spawnCount;  // The number of enemies of this type already spawned in this wave
        public GameObject enemyPrefab;

    }

    public List<Wave> waves; // a list of all the waves in the game
    public int currentWaveCount; // the index of the current wave , remember a list starts from 0


    [Header("Spawner Attributes")]
    float spawnTimer;  // timer use to  determine  when to spawn the next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed;   // the maximum number of enemies allowed on the map at once
    public bool maxEnemiesReached = false;  // a flag indicating if the maximum number of enemies has been reached
    public float waveInterval;      // the interval between each  wave

    [Header("Spawner Positions")]
    public List<Transform> relativeSpawnPoints; // a list to store all the relative spawn points of enemies


    Transform player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) //check if the waves has ended and the next wave should start
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        // check if its time to spawn the next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        Debug.Log($"Spawn Timer: {spawnTimer}, Interval: {waves[currentWaveCount].spawnInterval}"); 

    }

    IEnumerator BeginNextWave()
    {
        //wave for 'waveInterval' seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        //IOf there are more waves to start after current wave,move on to the next wave
        if(currentWaveCount < waves.Count - 1 )
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning($"Wave {currentWaveCount} quota: {currentWaveQuota}");

    }

    /// <summary>
    /// this method will stop spawning enemies if the amount of enemies on the map is maximum
    /// the method will only spawn enemies in a particular wave until it is time for next wave's enemies to be spawned
    /// </summary>

    void SpawnEnemies()
    {
        Debug.Log($"Current SpawnCount: {waves[currentWaveCount].spawnCount}, WaveQuota: {waves[currentWaveCount].waveQuota}, MaxEnemiesReached: {maxEnemiesReached}");

        // check if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            Debug.Log($"Spawning enemies: spawnCount = {waves[currentWaveCount].spawnCount}, waveQuota = {waves[currentWaveCount].waveQuota}, maxEnemiesReached = {maxEnemiesReached}");
            // spawn each type of the enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // check if  the minimum number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // limit the number of enemies that can be spawned at once
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        Debug.LogWarning("Max enemies reached!");
                        return;
                    }

                    // spawn the enemy at random position close to player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    Debug.Log($"Spawned {enemyGroup.enemyName}!");

                }
            }
        }

        //reset the maxEnemiesReached flag if the number of enemies alive has dropped below the maximum amount
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
            Debug.Log("Max enemies flag reset.");
        }
    }

    // call this function is enemy killed
    public void OnEnemyKilled()
    {
        // decrement the number of enemies alive
        enemiesAlive--;

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
            Debug.Log("Max enemies flag reset.");
        }
    }

}
