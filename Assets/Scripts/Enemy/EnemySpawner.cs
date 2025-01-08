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
        public int maxSimultaneousSpawns;
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
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) // Dalga baþlangýcý kontrolü
        {
            if (enemiesAlive > 0) // Aktif düþmanlar varsa dalgayý baþlatma
            {
                Debug.LogWarning("Enemies still alive, not starting the next wave!");
                return;
            }

            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn the next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        // Debug.Log($"Spawn Timer: {spawnTimer}, Interval: {waves[currentWaveCount].spawnInterval}");

    }

    IEnumerator BeginNextWave()
    {
        // Eðer dalga sýrasýnda hâlâ düþman varsa bekle
        while (enemiesAlive > 0 || waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            Debug.LogWarning("Waiting for all enemies to be cleared and spawned before starting the next wave...");
            yield return null;
        }

        // Dalgalar arasý bekleme süresi
        yield return new WaitForSeconds(waveInterval);

        // Eðer son dalgadaysak baþa dön
        if (currentWaveCount >= waves.Count - 1)
        {
            currentWaveCount = 0; // Sonsuz döngü için baþa dön
        }
        else
        {
            currentWaveCount++; // Bir sonraki dalgaya geç
        }

        CalculateWaveQuota(); // Yeni dalga için quota hesapla
        Debug.Log($"Wave {currentWaveCount + 1} is starting!");
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

        if (relativeSpawnPoints == null || relativeSpawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available!");
            return;
        }

        // Eðer dalganýn spawn kotasý dolmuþsa
        if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            Debug.Log($"Wave {currentWaveCount + 1} complete! SpawnCount: {waves[currentWaveCount].spawnCount}, WaveQuota: {waves[currentWaveCount].waveQuota}");
            StartCoroutine(HandleWaveCompletion());
            return;
        }

        // Eðer maksimum eþ zamanlý düþman sayýsýna ulaþýldýysa
        if (enemiesAlive >= waves[currentWaveCount].maxSimultaneousSpawns)
        {
            Debug.LogWarning("Max simultaneous spawns reached for this wave!");
            return;
        }

        // Ayný anda spawn edilecek düþman sayýsýný hesapla
        int spawnableEnemies = Mathf.Min(
            waves[currentWaveCount].maxSimultaneousSpawns - enemiesAlive, // Maksimum eþ zamanlý spawn sýnýrý
            waves[currentWaveCount].waveQuota - waves[currentWaveCount].spawnCount // Kalan spawn hakký
        );

        Debug.Log($"Spawning up to {spawnableEnemies} enemies...");

        for (int i = 0; i < spawnableEnemies; i++)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // Eðer bu gruptan daha fazla spawn edilebilecek düþman varsa
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // Düþmaný spawn et
                    Instantiate(
                        enemyGroup.enemyPrefab,
                        player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,
                        Quaternion.identity
                    );

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    Debug.Log($"Spawned {enemyGroup.enemyName}! Active enemies: {enemiesAlive}");

                    // Spawn edilen düþman sayýsý maksimum eþ zamanlý spawn sayýsýna ulaþtýysa döngüyü kýr
                    if (enemiesAlive >= waves[currentWaveCount].maxSimultaneousSpawns)
                    {
                        break;
                    }
                }
            }

            // Eðer düþmanlar maksimum eþ zamanlý spawn sayýsýna ulaþtýysa döngüyü kýr
            if (enemiesAlive >= waves[currentWaveCount].maxSimultaneousSpawns)
            {
                break;
            }
        }
    }

    IEnumerator HandleWaveCompletion()
    {
        Debug.Log($"Wave {currentWaveCount + 1} completed!");

        // Dalga tamamlandýktan sonra kýsa bir bekleme süresi
        yield return new WaitForSeconds(5f);

        // Eðer hâlâ düþman varsa yeni dalgaya geçme
        if (enemiesAlive > 0)
        {
            Debug.LogWarning("Enemies still alive! Cannot proceed to the next wave.");
            yield break; // Dalgayý sonlandýr ve yeni dalgaya geçme
        }

        // Yeni dalgaya geç
        currentWaveCount++;
        if (currentWaveCount >= waves.Count) // Son dalgaya ulaþýldýysa döngüyü baþa al
        {
            currentWaveCount = 0; // Sonsuz döngü için dalgalarý baþa al
        }

        // Dalganýn tüm sayaçlarýný sýfýrla
        foreach (var group in waves[currentWaveCount].enemyGroups)
        {
            group.spawnCount = 0; // Her grubun spawn sayýsýný sýfýrla
        }

        waves[currentWaveCount].spawnCount = 0; // Dalganýn genel spawn sayacýný sýfýrla
        enemiesAlive = 0; // Aktif düþmanlarý sýfýrla
        maxEnemiesReached = false;

        Debug.Log($"Wave {currentWaveCount + 1} is starting!");
    }

    // call this function is enemy killed
    public void OnEnemyKilled()
    {
        if (enemiesAlive > 0)
        {
            enemiesAlive--;
        }

        Debug.Log($"Enemy killed! Remaining enemies alive: {enemiesAlive}");

        // Eðer düþman sayýsý maksimumdan azsa bayraðý sýfýrla
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
            Debug.Log("Max enemies flag reset.");
        }

    }


}





