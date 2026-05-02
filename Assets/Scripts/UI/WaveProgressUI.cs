using UnityEngine;
using UnityEngine.UI;

public class WaveProgressUI : MonoBehaviour
{
    public Slider slider;
    public WaveManager waveManager;

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
        if (waveManager == null) return;
        slider.value = waveManager.KilledEnemies;
    }

    void OnWaveStart()
    {
        slider.minValue = 0;
        slider.maxValue = waveManager.TotalEnemiesInWave;
        slider.value = 0;
    }
}