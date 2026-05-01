//  Пуля 
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;
    public int damage = 1;
    Vector2 dir;

    public void Init(Vector2 direction)
    {
        dir = direction.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update() => transform.Translate(dir * speed * Time.deltaTime, Space.World);

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IEnemy>(out var enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}