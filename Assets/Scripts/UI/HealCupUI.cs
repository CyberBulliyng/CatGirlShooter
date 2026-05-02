using UnityEngine;
using UnityEngine.UI;

public class HealCupUI : MonoBehaviour
{
    public PlayerHeal playerHeal;
    public Image cupImage;
    public Sprite[] fillSprites;

    void Start()
    {
        playerHeal.OnChargeChanged += UpdateUI;
        UpdateUI(0, playerHeal.maxCharge);
    }

    void OnDestroy()
    {
        playerHeal.OnChargeChanged -= UpdateUI;
    }

    void UpdateUI(int current, int max)
    {
        current = Mathf.Clamp(current, 0, fillSprites.Length - 1);
        cupImage.sprite = fillSprites[current];
    }
}
