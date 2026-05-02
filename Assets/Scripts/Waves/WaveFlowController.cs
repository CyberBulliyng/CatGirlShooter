using UnityEngine;

public class LevelFlowController : MonoBehaviour
{
    [Header("Refs")]
    public WaveManager waveManager;

    [Header("Zones")]
    public ZonesController place11;
    public ZonesController place12;
    public ZonesController place21;
    public ZonesController place22;

    [Header("Optional")]
    public ZonesController centerZone; // всегда активна (можно не трогать)

    [Header("Statues")]
    public GameObject statue1;
    public GameObject statue2;
    public GameObject statue3;

    void Start()
    {
        // начальное состояние
        SetupInitialState();

        // подписка на завершение волн
        waveManager.onWaveComplete.AddListener(OnWaveComplete);
    }

    void SetupInitialState()
    {
        // центр не трогаем

        place11.SetState(ZoneState.Active);

        place12.SetState(ZoneState.Locked);
        place21.SetState(ZoneState.Locked);
        place22.SetState(ZoneState.Locked);
    }

    void OnWaveComplete(int waveNumber)
    {
        switch (waveNumber)
        {
            // 🟣 После 1 волны
            case 1:
                place11.SetState(ZoneState.Cleared);
                place22.SetState(ZoneState.Active);

                BreakStatue(statue1);
                break;

            // 🟣 После 2 волны
            case 2:
                place22.SetState(ZoneState.Cleared);
                place12.SetState(ZoneState.Active);

                BreakStatue(statue2);
                break;

            // 🟣 После 3 волны
            case 3:
                place12.SetState(ZoneState.Cleared);
                place21.SetState(ZoneState.Active);

                BreakStatue(statue3);
                break;

            // 🟣 После финальной волны
            case 4:
                place21.SetState(ZoneState.Cleared);

                OnGameCompleted();
                break;
        }
    }

    void BreakStatue(GameObject statue)
    {
        if (statue != null)
        {
            statue.SetActive(false); // или анимация
        }
    }

    void OnGameCompleted()
    {
        Debug.Log("GAME COMPLETED");

        // тут потом:
        // - запуск финального комикса
        // - переход сцены
    }
}