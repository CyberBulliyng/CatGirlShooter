using System.Collections;
using UnityEngine;

public class EnemySpawnEffect : MonoBehaviour
{
    [Header("Spawn Effect")]
    public GameObject dirtSpritePrefab;     // обычная земля вокруг
    public SpriteMask dirtMaskPrefab;       //  Новый префаб с SpriteMask

    public float riseDistance = 1.8f;
    public float riseDuration = 0.8f;

    private SpriteMask currentMask;
    private Transform cachedTransform;

    private AudioSource sourceEnemy;
    [SerializeField] private AudioClip audioBirthClip;

    void Awake()
    {
        cachedTransform = transform;
        sourceEnemy = GetComponent<AudioSource>();
    }

    public IEnumerator PlaySpawnEffect()
    {
        if (cachedTransform == null) yield break;

        Vector3 spawnPos = transform.position;
        GameObject dirt = null;

        sourceEnemy.PlayOneShot(audioBirthClip);
        // 1. Создаём обычную землю (фон)
        if (dirtSpritePrefab != null)
        {
            dirt = Instantiate(dirtSpritePrefab, spawnPos, Quaternion.identity);
        }

        // 2. Создаём маску (дыру)
        if (dirtMaskPrefab != null)
        {
            currentMask = Instantiate(dirtMaskPrefab, spawnPos, Quaternion.identity);
        }

        // 3. Прячем врага под землю
        Vector3 hiddenPos = spawnPos - new Vector3(0, riseDistance, 0);
        if (cachedTransform == null) yield break;
        cachedTransform.position = hiddenPos;

        var col = GetComponent<Collider2D>();
        var rb = GetComponent<Rigidbody2D>();

        if (col) col.enabled = false;
        if (rb) rb.linearVelocity = Vector2.zero;

        // 4. Вылезаем
        float elapsed = 0f;
        while (elapsed < riseDuration)
        {
            if (cachedTransform == null) yield break;

            elapsed += Time.deltaTime;
            float t = elapsed / riseDuration;
            cachedTransform.position = Vector3.Lerp(
                            hiddenPos,
                            spawnPos,
                            Mathf.SmoothStep(0f, 1f, t)
                        ); 
            yield return null;
        }

        if (cachedTransform == null) yield break;
        cachedTransform.position = spawnPos;

        if (col) col.enabled = true;

        // 5. Убираем маску и землю
        if (currentMask != null)
        {
            Destroy(currentMask.gameObject, 0.4f);
            Destroy(dirt.gameObject, 0.4f);
        }

        yield return new WaitForSeconds(0.1f);
    }
}