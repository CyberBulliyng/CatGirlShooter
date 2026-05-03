using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneTransition.SwitchToScene("Game");
    }

    public void StartIntro()
    {
        SceneTransition.SwitchToScene("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}