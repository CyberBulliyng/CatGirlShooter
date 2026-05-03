using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI панели")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private InputActionReference pauseAction;

    private bool GameIsPause = false;

    private void OnEnable()
    {
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
    }

    void Update()
    {

        if (pauseAction.action.triggered)
        {
            if (GameIsPause)
            {
                ContinueGame();
            }
            else
            {

                PauseGame();

            }
        }


    }

    public void ShowLosePanel()
    {
        GameState.IsPaused = true;
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        losePanel.SetActive(true);
        winPanel.SetActive(false);
    }

    public void ShowWinPanel()
    {
        GameState.IsPaused = true;
        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        GameIsPause = false;
        GameState.IsPaused = false;
        Time.timeScale = 1.0f;
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void PauseGame()
    {
        GameIsPause = true;
        GameState.IsPaused = true;
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneTransition.SwitchToScene(SceneManager.GetActiveScene().name);
    }

    public void BackMenu()
    {
        GameIsPause = false;
        Time.timeScale = 1.0f;
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        SceneTransition.SwitchToScene("MainMenu");
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
