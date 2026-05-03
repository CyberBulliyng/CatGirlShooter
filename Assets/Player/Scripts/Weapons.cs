using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

//  Базовое оружие 
public abstract class WeaponBase : MonoBehaviour
{
    [Header("Base Weapon")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;   // выстрелов в секунду
    protected float nextFireTime;

    [Header("Sounds")]
    public AudioClip[] shootSounds;
    AudioSource audioSource;

    protected PlayerController player;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = .7f;
    }

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

    protected void PlayRandomShootSound()
    {
        if (shootSounds.Length == 0) return;
        audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);
    }
}



