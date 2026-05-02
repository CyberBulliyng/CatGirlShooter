using UnityEngine;

public class FogIDLE : MonoBehaviour
{
    SpriteRenderer sr;
    Vector3 startPos;

    [Header("Movement")]
    public float moveAmplitude = 0.1f;
    public float moveSpeed = 0.5f;

    [Header("Alpha")]
    public float minAlpha = 0.4f;
    public float maxAlpha = 0.7f;
    public float alphaSpeed = 0.5f;

    float randomOffset;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float time = Time.time + randomOffset;
        float x = Mathf.Sin(time * moveSpeed) * moveAmplitude;
        float y = Mathf.Cos(time * moveSpeed * 0.7f) * moveAmplitude;

        transform.position = startPos + new Vector3(x, y, 0);
        float alpha = Mathf.Lerp(minAlpha, maxAlpha,
            (Mathf.Sin(time * alphaSpeed) + 1f) / 2f);

        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}