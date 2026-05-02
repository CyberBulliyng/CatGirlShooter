using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneTransition.SwitchToScene("Game");
    }
}