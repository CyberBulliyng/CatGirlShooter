//  Ружьё (дробовик) 
using System.Collections;
using UnityEngine;

public class Shotgun : WeaponBase
{
    [Header("Shotgun")]
    public int pelletCount = 6;
    public float spreadAngle = 20f;
    public float knockbackForce = 8f;
    public float chargeDuration = 0.15f;   // небольшая задержка «перед выстрелом»

    bool isCharging;

    public override bool CanShoot() => base.CanShoot() && !isCharging;

    public override void Shoot(Vector2 direction) => StartCoroutine(ShootRoutine(direction));

    IEnumerator ShootRoutine(Vector2 direction)
    {
        isCharging = true;
        nextFireTime = Time.time + 1f / fireRate;

        // Небольшая анимация «зарядки» — двигаем оружие назад
        Vector3 originPos = Vector3.zero;
        Vector3 kickBack = originPos + Vector3.left * 0.15f;

        float t = 0;
        while (t < chargeDuration)
        {
            transform.localPosition = Vector3.Lerp(originPos, kickBack, t / chargeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        // Выстрел
        for (int i = 0; i < pelletCount; i++)
        {
            float offset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            Vector2 pelletDir = Quaternion.Euler(0, 0, offset) * direction;
            var b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            b.GetComponent<Bullet>()?.Init(pelletDir);
        }

        // Отдача игрока
        player?.ApplyKnockback(direction, knockbackForce);

        // Возврат оружия в исходное положение
        t = 0;
        while (t < chargeDuration)
        {
            transform.localPosition = Vector3.Lerp(kickBack, originPos, t / chargeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
        isCharging = false;
    }
}