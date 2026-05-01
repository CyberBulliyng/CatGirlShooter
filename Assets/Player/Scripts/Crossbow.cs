//  Арбалет 
using UnityEngine;

public class Crossbow : WeaponBase
{
    public override void Shoot(Vector2 direction)
    {
        if (!CanShoot()) return;
        nextFireTime = Time.time + 1f / fireRate;

        var b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        b.GetComponent<Bullet>()?.Init(direction);
    }
}