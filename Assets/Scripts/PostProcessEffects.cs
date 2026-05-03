using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessEffects : MonoBehaviour
{
    public static PostProcessEffects instance;

    private Volume volume;
    private ColorAdjustments colorAdjustments;

    [Header("Damage")]
    public Color damageColor = new Color(1f, 0.2f, 0.2f, 1f);
    public float damageDuration = 0.25f;

    [Header("Heal")]
    public Color healColor = new Color(0.2f, 1f, 0.2f, 1f);
    public float healDuration = 0.4f;

    private Coroutine activeRoutine;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        volume = GetComponent<Volume>();

        if (volume == null)
        {
            Debug.LogError("Volume component not found on " + gameObject.name);
            return;
        }

        // КРИТИЧНО для изменения из кода!
        if (volume.profile == null)
        {
            Debug.LogError("Volume has no Profile assigned!");
            return;
        }

        // Используем sharedProfile
        VolumeProfile profile = volume.sharedProfile;

        if (!profile.TryGet(out colorAdjustments))
        {
            colorAdjustments = profile.Add<ColorAdjustments>(true);
        }

        // Включаем override
        colorAdjustments.active = true;
        colorAdjustments.colorFilter.overrideState = true;
        colorAdjustments.colorFilter.value = Color.white; // начальное значение
    }

    public static void PlayDamageEffect() => instance?.StartEffect(instance.damageColor, instance.damageDuration);
    public static void PlayHealEffect() => instance?.StartEffect(instance.healColor, instance.healDuration);

    void StartEffect(Color targetColor, float duration)
    {
        if (activeRoutine != null)
            StopCoroutine(activeRoutine);

        activeRoutine = StartCoroutine(FlashRoutine(targetColor, duration));
    }

    IEnumerator FlashRoutine(Color targetColor, float duration)
    {
        if (colorAdjustments == null) yield break;

        float half = duration / 2f;
        float t = 0f;

        // Нарастание
        while (t < half)
        {
            t += Time.deltaTime;
            float progress = t / half;
            colorAdjustments.colorFilter.value = Color.Lerp(Color.white, targetColor, progress);
            yield return null;
        }

        // Затухание
        t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            float progress = t / half;
            colorAdjustments.colorFilter.value = Color.Lerp(targetColor, Color.white, progress);
            yield return null;
        }

        colorAdjustments.colorFilter.value = Color.white;
    }
}