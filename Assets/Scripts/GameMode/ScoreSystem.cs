using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    int score = 0;

    public UnityEvent<int> onScoreChanged;

    public void AddScore(int amount)
    {
        score += amount;
        onScoreChanged?.Invoke(score);
    }

    public int GetScore() => score;
}