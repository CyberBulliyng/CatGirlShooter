using UnityEngine;
using UnityEngine.UI;

public class WeaponIconUI : MonoBehaviour
{
    public WeaponSwitcher switcher;
    public Image icon;
    public Sprite[] weaponIcons;

    void Start()
    {
        switcher.OnWeaponChanged += UpdateIcon;
        UpdateIcon(switcher.CurrentIndex);
    }

    void OnDestroy()
    {
        switcher.OnWeaponChanged -= UpdateIcon;
    }

    void UpdateIcon(int index)
    {
        icon.sprite = weaponIcons[index];
    }
}