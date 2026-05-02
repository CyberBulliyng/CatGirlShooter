using System.Collections;
using UnityEngine;
//using Cinemachine;
//using Cinemachine.Components;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects instance;

    //public CinemachineCamera vcam;

    //CinemachineBasicMultiChannelPerlin noise;
    //float baseOrthoSize;

    //void Awake()
    //{
    //    instance = this;
    //}

    //void Start()
    //{
    //    if (vcam == null)
    //        vcam = FindObjectOfType<CinemachineVirtualCamera>();

    //    noise = vcam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    //    baseOrthoSize = vcam.m_Lens.OrthographicSize;
    //}

    //public void PlayDamageEffect()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(DamageEffect());
    //}

    //IEnumerator DamageEffect()
    //{
    //    float duration = 0.2f;
    //    float t = 0;

    //    float targetSize = baseOrthoSize - 0.3f;

    //    noise.m_AmplitudeGain = 2f;
    //    noise.m_FrequencyGain = 3f;

    //    while (t < duration)
    //    {
    //        t += Time.deltaTime;
    //        vcam.m_Lens.OrthographicSize =
    //            Mathf.Lerp(baseOrthoSize, targetSize, t / duration);

    //        yield return null;
    //    }
    //    noise.m_AmplitudeGain = 0f;
    //    vcam.m_Lens.OrthographicSize = baseOrthoSize;
    //}
    //public void PlayWaveStartEffect()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(WaveEffect());
    //}

    //IEnumerator WaveEffect()
    //{
    //    noise.m_AmplitudeGain = 3f;
    //    noise.m_FrequencyGain = 2f;

    //    yield return new WaitForSeconds(0.5f);

    //    noise.m_AmplitudeGain = 0f;
    //}
}