// Базовый враг
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Enemy")]
    public int maxHealth = 3;
    public float moveSpeed = 2f;
    public float attackRange = 0.6f;
    public int contactDamage = 1;
    public float attackCooldown = 1.2f;     // увеличил чуть для баланса
    
    bool isDead = false;

    public GameObject healDropPrefab;
    public GameObject fountain;

    int health;
    float nextAttackTime;        // переименовал для ясности
    Transform player;
    Rigidbody2D rb;

    [Range(0f, 1f)]
    public float dropChance = 0.5f;

    public event System.Action OnDied;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        var p = PlayerController.instance;
        if (p) player = p.transform;

        if (fountain == null)
        {
            GameObject f = GameObject.FindWithTag("Fontaine");
            if (f != null) fountain = f;
        }
    }

    Transform GetTarget()
    {
        if (fountain == null) return player;

        float distToPlayer = Vector2.Distance(transform.position, player.position);
        float distToFountain = Vector2.Distance(transform.position, fountain.transform.position);

        return distToFountain < distToPlayer ? fountain.transform : player;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Transform target = GetTarget();
        float dist = Vector2.Distance(transform.position, target.position);

        if (dist > attackRange)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.6f, LayerMask.GetMask("Obstacle"));

            if (hit.collider != null)
            {
                Vector2 sideDir = new Vector2(-dir.y, dir.x);

                RaycastHit2D sideHit = Physics2D.Raycast(transform.position, sideDir, 0.6f, LayerMask.GetMask("Obstacle"));

                if (sideHit.collider == null)
                    dir = sideDir;
                else
                    dir = -sideDir;
            }
            rb.linearVelocity = dir * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            if (target == player)
                TryAttack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + attackCooldown;

        var playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        OnDied?.Invoke();
        if (Random.value <= dropChance)
        {
            Instantiate(healDropPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}