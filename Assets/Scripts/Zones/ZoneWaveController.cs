using UnityEngine;

public class ZoneWaveController : MonoBehaviour
{
    public WaveManager waveManager;
    public ZonesController[] zones;

    void Start()
    {
        // подписываемся на события волн
        waveManager.onWaveStart.AddListener(OnWaveStart);
        waveManager.onWaveComplete.AddListener(OnWaveComplete);

        // в начале всё закрыто
        foreach (var zone in zones)
            zone.SetState(ZoneState.Locked);
    }

    void OnWaveStart()
    {
        int waveIndex = waveManager.CurrentWave - 1;

        if (waveIndex < zones.Length)
        {
            zones[waveIndex].SetState(ZoneState.Active);
        }
    }

    void OnWaveComplete(int waveNumber)
    {
        int index = waveNumber - 1;

        if (index < zones.Length)
        {
            zones[index].SetState(ZoneState.Cleared);
        }
    }
}