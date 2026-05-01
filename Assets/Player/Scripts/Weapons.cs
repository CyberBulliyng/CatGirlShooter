using UnityEngine;
using System.Collections;

//  Базовое оружие 
public abstract class WeaponBase : MonoBehaviour
{
    [Header("Base Weapon")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;   // выстрелов в секунду
    protected float nextFireTime;

    protected PlayerController player;

    void Awake() => player = GetComponentInParent<PlayerController>();

    public virtual bool CanShoot() => Time.time >= nextFireTime;

    public abstract void Shoot(Vector2 direction);

    // Вращает оружие вслед за курсором (вызывается из WeaponSwitcher)
    public void AimAt(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Зеркалим спрайт оружия при взгляде влево
        Vector3 s = transform.localScale;
        s.y = (dir.x < 0) ? -1 : 1;
        transform.localScale = s;
    }
}



