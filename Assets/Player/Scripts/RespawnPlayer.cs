using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    private bool isLoad = false;

    private void Start()
    {
        if (PlayerHealth.instance != null)
        {
            // —начала отписываемс€ Ч защита от двойной подписки
            PlayerHealth.instance.OnRespawned -= Respawn;
            PlayerHealth.instance.OnRespawned += Respawn;
        }
    }

    private void OnDestroy()
    {
        if (PlayerHealth.instance != null)
            PlayerHealth.instance.OnRespawned -= Respawn;
    }

    private void Respawn()
    {
        if (isLoad) return;
        isLoad = true;
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().name);
    }
}
