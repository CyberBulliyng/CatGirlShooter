using UnityEngine;
using UnityEngine.UI;

public class WaveProgressUI : MonoBehaviour
{
    public Slider slider;
    public WaveManager waveManager;

    int totalEnemies = 0;

    void Start()
    {
        if (waveManager == null)
            waveManager = FindObjectOfType<WaveManager>();

        if (waveManager == null)
        {
            Debug.LogError("WaveManager not found!");
            return;
        }

        waveManager.onWaveStart.AddListener(OnWaveStart);
    }

    void Update()
    {
        if (waveManager == null || totalEnemies == 0) return;

        int alive = waveManager.AliveEnemies;

        slider.value = alive;
    }

    void OnWaveStart()
    {
        int waveIndex = waveManager.CurrentWave - 1;

        if (waveIndex < 0 || waveIndex >= waveManager.waves.Length)
            return;

        totalEnemies = waveManager.waves[waveIndex].enemyCount;

        slider.maxValue = totalEnemies;
        slider.value = totalEnemies;
    }
}