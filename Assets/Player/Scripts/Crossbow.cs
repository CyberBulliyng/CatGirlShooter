//  Àğáàëåò 
using UnityEngine;

public class Crossbow : WeaponBase
{
    public override void Shoot(Vector2 direction)
    {
        if (!CanShoot()) return;
        nextFireTime = Time.time + 1f / fireRate;

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        // === ÏÎÂÎĞÀ×ÈÂÀÅÌ ÑÍÀĞßÄ Â ÍÓÆÍÎÌ ÍÀÏĞÀÂËÅÍÈÈ ===
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletGO.transform.rotation = Quaternion.Euler(0, 0, angle-90);


        bullet?.Init(direction);
    }
}