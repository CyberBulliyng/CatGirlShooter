using UnityEngine;

public class StatueManager : MonoBehaviour
{
    public StatueController[] statues;
    public WaveManager waveManager;

    void Start()
    {
        if (waveManager == null)
            waveManager = FindObjectOfType<WaveManager>();

        if (waveManager == null)
        {
            Debug.LogError("WaveManager NOT FOUND");
            return;
        }

        // Подписка на события
        waveManager.onWaveStart.AddListener(OnWaveStart);
        waveManager.onWaveComplete.AddListener(OnWaveEnd);
    }

    void OnWaveStart()
    {
        int wave = waveManager.CurrentWave;
        Debug.Log("Wave START: " + wave);

        SetAll(false);

        if (wave == 1) statues[0].SetActive(true);
        else if (wave == 2) statues[1].SetActive(true);
        else if (wave == 3) statues[2].SetActive(true);
        else if (wave == 4)
        {
            foreach (var s in statues)
                s.SetActive(true);
        }
    }

    void OnWaveEnd(int wave)
    {
        Debug.Log("Wave END: " + wave);
        SetAll(false);
    }

    void SetAll(bool state)
    {
        foreach (var s in statues)
            s.SetActive(state);
    }
}