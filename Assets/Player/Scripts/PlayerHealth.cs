using UnityEngine;
using System.Collections;
using System;

//  «доровье игрока / смерть / возрождение 
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("Health")]
    public int maxHealth = 5;
    public float respawnDelay = 2f;
    public Transform respawnPoint;       // если null Ч возрождаетс€ на стартовой позиции

    int currentHealth;
    bool isDead;
    private bool _isDying = false;
    private bool _isRespawning = false;
    Vector3 startPos;

    // UI Ч можно подписатьс€ на это событие снаружи
    public event Action<int, int> OnHealthChanged; // (current, max)
    public event Action OnDied;
    public event Action OnRespawned;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        startPos = transform.position;
    }

    public void TakeDamage(int amount)
    {
        if (isDead || _isDying) return;
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0) StartCoroutine(DieRoutine());

    }

    IEnumerator DieRoutine()
    {
        isDead = true;
        OnDied?.Invoke();

        //gameObject.SetActive(false);

        // ќтключаем управление
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<WeaponSwitcher>().enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;

        // “ут можно проиграть анимацию смерти / спр€тать объект
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);
        Respawn();
        _isDying = false;
    }

    void Respawn()
    {
        if (_isRespawning) return;
        _isRespawning = true;
        /*transform.position = respawnPoint != null ? respawnPoint.position : startPos;
        currentHealth = maxHealth;
        isDead = false;

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        GetComponent<WeaponSwitcher>().enabled = true;*///ѕока что закоменчено потому что мы ещЄ не решили когда возвращать игрока в начало или в чек поинт

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnRespawned?.Invoke();
    }
}
