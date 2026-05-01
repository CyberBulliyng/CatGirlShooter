//  Базовый враг 
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Enemy")]
    public int maxHealth = 3;
    public float moveSpeed = 2f;
    public float attackRange = 0.6f;
    public int contactDamage = 1;
    public float attackCooldown = 1f;

    int health;
    float nextAttack;
    Transform player;
    Rigidbody2D rb;

    public event System.Action OnDied;
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        var p = PlayerController.instance;
        if (p) player = p.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > attackRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Time.time < nextAttack) return;
        nextAttack = Time.time + attackCooldown;
        player.GetComponent<PlayerHealth>()?.TakeDamage(contactDamage);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        OnDied?.Invoke();
        Destroy(gameObject);
    }
}