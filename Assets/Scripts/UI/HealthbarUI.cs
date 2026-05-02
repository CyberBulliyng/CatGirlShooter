using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    [Header("Refs")]
    public Image[] nodes;
    public Sprite fullSprite;
    public Sprite halfSprite;
    public Sprite emptySprite;

    [Header("Target")]
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth.OnHealthChanged += UpdateHealth;

        // сразу обновить UI
        UpdateHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    void OnDestroy()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
    }

    void UpdateHealth(int hp, int maxHp)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            int hpForNode = Mathf.Clamp(hp - (i * 2), 0, 2);

            if (hpForNode == 2)
                nodes[i].sprite = fullSprite;
            else if (hpForNode == 1)
                nodes[i].sprite = halfSprite;
            else
                nodes[i].sprite = emptySprite;
        }
    }
}
