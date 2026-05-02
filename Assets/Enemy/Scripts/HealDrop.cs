using UnityEngine;

public class HealDrop : MonoBehaviour
{
    public int value = 1;

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.TryGetComponent(out PlayerHeal player))
    //    {
    //        player.AddCharge(value);
    //        Destroy(gameObject);
    //    }
    //}
    bool picked = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (picked) return;

        if (other.GetComponentInParent<PlayerHeal>() != null)
        {
            picked = true;

            var player = other.GetComponentInParent<PlayerHeal>();
            player.AddCharge(value);

            Destroy(gameObject);
        }
    }
}
