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
        public float spawnInterval = 0.5f;
    }

    [Header("Waves")]
    public Wave[] waves;
    public float timeBetweenWaves = 3f;

    [Header("Events")]
    public UnityEvent onWaveStart;
    public UnityEvent<int> onWaveComplete;
    public UnityEvent onAllWavesComplete;

    [Header("Background")]
    public SpriteRenderer backgroundRenderer;
    public Sprite[] waveBackgrounds;

    int currentWave = 0;
    int aliveEnemies = 0;
    bool isSpawning = false;

    int totalEnemiesInWave = 0;
    int killedEnemies = 0;
    public int KilledEnemies => killedEnemies;
    public int TotalEnemiesInWave => totalEnemiesInWave;

    void Start() => StartCoroutine(RunWaves());

    IEnumerator RunWaves()
    {
        foreach (Wave wave in waves)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            totalEnemiesInWave = wave.enemyCount;
            killedEnemies = 0;

            yield return StartCoroutine(SpawnWave(wave));

            yield return new WaitUntil(() => aliveEnemies <= 0 && !isSpawning);

            onWaveComplete?.Invoke(currentWave + 1);
            currentWave++;
        }

        onAllWavesComplete?.Invoke();
        FindObjectOfType<GameManager>().Win();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;
        if (backgroundRenderer != null && waveBackgrounds.Length > 0)
        {
            int index = Mathf.Min(currentWave, waveBackgrounds.Length - 1);
            Sprite newSprite = waveBackgrounds[index];

            StartCoroutine(ChangeBackgroundSmooth(newSprite));
        }

        onWaveStart?.Invoke();
        //CameraEffects.instance?.PlayWaveStartEffect();
        for (int i = 0; i < wave.enemyCount; i++)
        {
            Transform spawnPoint = wave.spawnPoints[Random.Range(0, wave.spawnPoints.Length)];

            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoint.position, Quaternion.identity);

            var enemyScript = enemy.GetComponent<Enemy>();
            var spawnEffect = enemy.GetComponent<EnemySpawnEffect>();

            // сначала эффект
            if (spawnEffect != null)
            {
                yield return StartCoroutine(spawnEffect.PlaySpawnEffect());
            }
            else
            {
                Debug.LogWarning("EnemySpawnEffect component missing on enemy prefab!");
            }

            // потом регистрация
            if (enemyScript != null)
            {
                enemyScript.OnDied += HandleEnemyDied;
                aliveEnemies++;
            }

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
    }

    IEnumerator ChangeBackgroundSmooth(Sprite newSprite)
    {
        float t = 0f;
        float duration = 0.3f;

        Color c = backgroundRenderer.color;

        // fade out
        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / duration);
            backgroundRenderer.color = c;
            yield return null;
        }

        backgroundRenderer.sprite = newSprite;

        t = 0f;

        // fade in
        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / duration);
            backgroundRenderer.color = c;
            yield return null;
        }
    }

    void HandleEnemyDied()
    {
        if (killedEnemies >= totalEnemiesInWave) return;
        aliveEnemies = Mathf.Max(0, aliveEnemies - 1);
        killedEnemies++;
        Debug.Log($"Killed: {killedEnemies} / {totalEnemiesInWave}");
    }

    public float WaveProgress =>
        totalEnemiesInWave == 0 ? 0 : (float)killedEnemies / totalEnemiesInWave;

    // Геттеры для UI
    public int AliveEnemies => aliveEnemies;
    public int CurrentWave => currentWave + 1;
    public int TotalWaves => waves.Length;
}