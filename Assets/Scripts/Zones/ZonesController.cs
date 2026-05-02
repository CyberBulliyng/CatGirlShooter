using UnityEngine;

public enum ZoneState
{
    Locked,
    Active,
    Cleared
}

public class ZonesController : MonoBehaviour
{
    [Header("Refs")]
    public GameObject fog;
    public GameObject blocker;
    public GameObject lights;
    //public Transform[] spawnPoints;

    public ZoneState currentState = ZoneState.Locked;

    public void SetState(ZoneState state)
    {
        currentState = state;

        switch (state)
        {
            case ZoneState.Locked:
                if (fog) fog.SetActive(true);
                if (blocker) blocker.SetActive(true);
                if (lights) lights.SetActive(false);
                break;

            case ZoneState.Active:
                if (fog) fog.SetActive(false);
                if (blocker) blocker.SetActive(false);
                if (lights) lights.SetActive(false);
                break;

            case ZoneState.Cleared:
                if (fog) fog.SetActive(false);
                if (blocker) blocker.SetActive(false);
                if (lights) lights.SetActive(true);
                break;
        }
    }
}