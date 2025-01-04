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
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) // Dalga ba�lang�c� kontrol�
        {
            if (enemiesAlive > 0) // Aktif d��manlar varsa dalgay� ba�latma
            {
                Debug.LogWarning("Enemies still alive, not starting the next wave!");
                return;
            }

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
        // E�er dalga s�ras�nda h�l� d��man varsa bekle
        while (enemiesAlive > 0)
        {
            Debug.LogWarning("Waiting for all enemies to be cleared before starting the next wave...");
            yield return null;
        }

        // Dalgalar aras� bekleme s�resi
        yield return new WaitForSeconds(waveInterval);

        // E�er son dalgadaysak ba�a d�n
        if (currentWaveCount >= waves.Count - 1)
        {
            currentWaveCount = 0; // Sonsuz d�ng� i�in ba�a d�n
        }
        else
        {
            currentWaveCount++; // Bir sonraki dalgaya ge�
        }

        CalculateWaveQuota(); // Yeni dalga i�in quota hesapla
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

        // E�er spawn noktalar� yoksa hata ver
        if (relativeSpawnPoints == null || relativeSpawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available!");
            return;
        }

        if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            Debug.Log($"Wave {currentWaveCount + 1} complete! SpawnCount: {waves[currentWaveCount].spawnCount}, WaveQuota: {waves[currentWaveCount].waveQuota}");
            StartCoroutine(HandleWaveCompletion());
            return;
        }

        // E�er maksimum d��man say�s�na ula��ld�ysa spawn i�lemini durdur
        if (enemiesAlive >= maxEnemiesAllowed)
        {
            maxEnemiesReached = true;
            Debug.LogWarning("Max enemies reached!");
            return;
        }

        // Her d��man grubunda spawn i�lemi yap
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            // E�er bu grubun d��man say�s� tamamlanmam��sa d��man olu�tur
            if (enemyGroup.spawnCount < enemyGroup.enemyCount)
            {
                Instantiate(enemyGroup.enemyPrefab,
                            player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,
                            Quaternion.identity);

                enemyGroup.spawnCount++;
                waves[currentWaveCount].spawnCount++;
                enemiesAlive++;

                Debug.Log($"Spawned {enemyGroup.enemyName}!");
            }
        }

        // Aktif d��man say�s� azald���nda maxEnemiesReached bayra��n� s�f�rla
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
            Debug.Log("Max enemies flag reset.");
        }
    }

    IEnumerator HandleWaveCompletion()
    {
        Debug.Log($"Wave {currentWaveCount + 1} completed!");

        yield return new WaitForSeconds(5f); // Dalga tamamland�ktan sonra k�sa bir bekleme s�resi

        // Yeni dalgaya ge�
        currentWaveCount++;
        if (currentWaveCount >= waves.Count) // Son dalgaya ula��ld�ysa d�ng�y� ba�a al
        {
            currentWaveCount = 0; // Sonsuz d�ng� i�in dalgalar� ba�a al
        }

        // Dalgan�n t�m saya�lar�n� s�f�rla
        foreach (var group in waves[currentWaveCount].enemyGroups)
        {
            group.spawnCount = 0;
        }

        waves[currentWaveCount].spawnCount = 0; // Dalgan�n spawn sayac�n� s�f�rla
        enemiesAlive = 0; // Aktif d��manlar� s�f�rla
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

        // E�er d��man say�s� maksimumdan azsa bayra�� s�f�rla
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
            Debug.Log("Max enemies flag reset.");
        }

    }


}





