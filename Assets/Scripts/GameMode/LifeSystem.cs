using UnityEngine;
using UnityEngine.Events;

public class LifeSystem : MonoBehaviour
{
    public int maxLives = 9;
    int currentLives;

    public UnityEvent<int> onLivesChanged;
    public UnityEvent onNoLives;

    void Start()
    {
        currentLives = maxLives;
        onLivesChanged?.Invoke(currentLives);
    }

    public void LoseLife()
    {
        currentLives--;

        onLivesChanged?.Invoke(currentLives);

        if (currentLives <= 0)
        {
            onNoLives?.Invoke();
        }
    }

    public int GetLives() => currentLives;
}