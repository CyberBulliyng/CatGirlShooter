using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemyCount = 5;
        public GameObject enemyPrefab;
        public Transform[] spawnPoints;
        public float spawnInterval = 0.5f;   // задержка между спавном каждого врага
    }

    [Header("Waves")]
    public Wave[] waves;
    public float timeBetweenWaves = 3f;

    [Header("Events")]
    public UnityEvent onWaveStart;           // начало волны
    public UnityEvent<int> onWaveComplete;   // волна завершена (номер волны)
    public UnityEvent onAllWavesComplete;    // все волны пройдены

    int currentWave = 0;
    int aliveEnemies = 0;
    bool isSpawning = false;

    void Start() => StartCoroutine(RunWaves());

    IEnumerator RunWaves()
    {
        foreach (Wave wave in waves)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return StartCoroutine(SpawnWave(wave));

            // Ждём пока все враги умрут
            yield return new WaitUntil(() => aliveEnemies <= 0 && !isSpawning);

            onWaveComplete?.Invoke(currentWave + 1);
            currentWave++;
        }

        onAllWavesComplete?.Invoke();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;
        onWaveStart?.Invoke();

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Transform spawnPoint = wave.spawnPoints[Random.Range(0, wave.spawnPoints.Length)];

            // Спавним врага
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoint.position, Quaternion.identity);

            var enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.OnDied += HandleEnemyDied;
                aliveEnemies++;
            }

            // Запускаем эффект появления
            var spawnEffect = enemy.GetComponent<EnemySpawnEffect>();
            if (spawnEffect != null)
            {
                yield return StartCoroutine(spawnEffect.PlaySpawnEffect()); // ЖДЁМ, пока эффект полностью закончится
            }
            else
            {
                Debug.LogWarning("EnemySpawnEffect component missing on enemy prefab!");
            }

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
    }

    void HandleEnemyDied() => aliveEnemies--;

    // Геттеры для UI
    public int AliveEnemies => aliveEnemies;
    public int CurrentWave => currentWave + 1;
    public int TotalWaves => waves.Length;
}