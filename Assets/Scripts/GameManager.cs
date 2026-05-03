using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    public PauseMenu menu;

    public void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("YOU LOSE");

        Time.timeScale = 0f;

        menu.ShowLosePanel();
        // потом тут будет UI
        // ShowLoseScreen();
    }

    public void Win()
    {
        isGameOver = true;
        PlayerPrefs.SetInt("9LivesUnlocked", 1);

        Debug.Log("YOU WIN");

        Time.timeScale = 0f;

        menu.ShowWinPanel();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}