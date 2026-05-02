using UnityEngine;

public class HealDrop : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHeal player))
        {
            player.AddCharge(value);
            Destroy(gameObject);
        }
    }
}
