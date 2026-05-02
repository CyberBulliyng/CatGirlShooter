using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    public WeaponBase[] weapons;

    [Header("Input")]
    public InputActionReference shootAction;
    public InputActionReference lookAction;
    public InputActionReference scrollAction;
    public InputActionReference weapon1Action;
    public InputActionReference weapon2Action;

    int currentIndex = 0;
    WeaponBase current;

    public System.Action<int> OnWeaponChanged;
    public int CurrentIndex => currentIndex;
    public WeaponBase CurrentWeapon => current;

    void OnEnable()
    {
        shootAction.action.Enable();
        lookAction.action.Enable();
        scrollAction.action.Enable();
        weapon1Action.action.Enable();
        weapon2Action.action.Enable();
    }

    void OnDisable()
    {
        shootAction.action.Disable();
        lookAction.action.Disable();
        scrollAction.action.Disable();
        weapon1Action.action.Disable();
        weapon2Action.action.Disable();
    }

    void Start() => EquipWeapon(0);

    void Update()
    {
        if (GameState.IsPaused) return;
        // Переключение колёсиком
        float scroll = scrollAction.action.ReadValue<Vector2>().y;
        if (scroll > 0) EquipWeapon((currentIndex + 1) % weapons.Length);
        if (scroll < 0) EquipWeapon((currentIndex - 1 + weapons.Length) % weapons.Length);

        // Переключение клавишами
        if (weapon1Action.action.WasPressedThisFrame()) EquipWeapon(0);
        if (weapon2Action.action.WasPressedThisFrame()) EquipWeapon(1);

        if (current == null) return;

        // Прицел
        Vector2 mouseScreen = lookAction.action.ReadValue<Vector2>();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreen.x, mouseScreen.y, -Camera.main.transform.position.z)
        );
        Vector2 dir = ((Vector2)mouseWorld - (Vector2)transform.position).normalized;
        current.AimAt(dir);

        // Стрельба — проверяем CanShoot() перед вызовом
        if (shootAction.action.IsPressed() && current.CanShoot())
            current.Shoot(dir);
    }

    void EquipWeapon(int index)
    {
        if (index == currentIndex && current != null) return;
        foreach (var w in weapons) w.gameObject.SetActive(false);
        currentIndex = index;
        current = weapons[index];
        current.gameObject.SetActive(true);
        OnWeaponChanged?.Invoke(currentIndex);

        // Передаём новый SpriteRenderer оружия в PlayerController
        var pc = GetComponent<PlayerController>();
        if (pc != null)
            pc.weaponRenderer = current.GetComponentInChildren<SpriteRenderer>();
    }
}