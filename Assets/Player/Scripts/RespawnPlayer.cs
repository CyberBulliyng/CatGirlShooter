using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    private bool isLoad = false;

    private void Start()
    {
        if (PlayerHealth.instance != null)
        {
            PlayerHealth.instance.OnDied -= OnPlayerDied;
            PlayerHealth.instance.OnDied += OnPlayerDied;
        }
    }

    void OnPlayerDied()
    {
        FindObjectOfType<GameManager>().Lose();
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
