using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;

    public void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("YOU LOSE");

        Time.timeScale = 0f;

        // потом тут будет UI
        // ShowLoseScreen();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}