using UnityEngine;
using System.Collections;

public class GameMode_9Lives : MonoBehaviour
{
    public LifeSystem lifeSystem;
    public ScoreSystem scoreSystem;
    public GameManager gameManager;

    void Start()
    {
        var player = FindObjectOfType<PlayerHealth>();

        player.is9LivesMode = true;
        player.OnDied += OnPlayerDeath;

        lifeSystem.onNoLives.AddListener(OnGameOver);
    }

    public void RegisterEnemy(Enemy enemy)
    {
        enemy.dropChance = 0f;
        enemy.OnDied += OnEnemyKilled;
    }

    void OnEnemyKilled()
    {
        scoreSystem.AddScore(10);
    }

    void OnPlayerDeath()
    {
        lifeSystem.LoseLife();

        if (lifeSystem.GetLives() > 0)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        var player = FindObjectOfType<PlayerHealth>();
        player.ForceRespawn();
    }

    void OnGameOver()
    {
        SaveRecord();
        gameManager.Lose();
    }

    void SaveRecord()
    {
        int current = scoreSystem.GetScore();
        int record = PlayerPrefs.GetInt("9LivesRecord", 0);

        if (current > record)
        {
            PlayerPrefs.SetInt("9LivesRecord", current);
        }
    }
}