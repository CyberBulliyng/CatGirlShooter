using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonFX : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    Vector3 baseScale;
    Quaternion baseRotation;
    Image img;
    Color baseColor;

    [Header("Scale")]
    public float hoverScale = 1.05f;
    public float clickScale = 0.95f;

    [Header("Rotation")]
    public float hoverRotation = 3f;
    public float clickRotation = -5f;

    [Header("Color")]
    public float hoverBrightness = 1.02f;
    public float clickBrightness = 0.85f;

    void Start()
    {
        baseScale = transform.localScale;
        baseRotation = transform.localRotation;

        img = GetComponent<Image>();
        if (img != null)
            baseColor = img.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = baseScale * hoverScale;
        transform.localRotation = baseRotation * Quaternion.Euler(0, 0, hoverRotation);

        SetBrightness(hoverBrightness);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = baseScale;
        transform.localRotation = baseRotation;

        SetBrightness(1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = baseScale * clickScale;
        transform.localRotation = baseRotation * Quaternion.Euler(0, 0, clickRotation);

        SetBrightness(clickBrightness);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = baseScale * hoverScale;
        transform.localRotation = baseRotation * Quaternion.Euler(0, 0, hoverRotation);

        SetBrightness(hoverBrightness);
    }

    void SetBrightness(float multiplier)
    {
        if (img == null) return;

        Color c = baseColor * multiplier;
        c.a = baseColor.a;
        img.color = c;
    }
}