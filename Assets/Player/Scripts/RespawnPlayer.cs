using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{

    private void Start()
    {
        if (PlayerHealth.instance != null)
            PlayerHealth.instance.OnRespawned += Respawn;
    }

    private void OnDestroy()
    {
        if (PlayerHealth.instance != null)
            PlayerHealth.instance.OnRespawned -= Respawn;
    }

    private void Respawn()
    {
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().name);
    }
}
