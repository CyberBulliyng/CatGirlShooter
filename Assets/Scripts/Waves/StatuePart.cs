using UnityEngine;

public class StatuePart : MonoBehaviour
{
    public SpriteRenderer[] renderers; // ВСЕ части статуи

    public Sprite offSprite;
    public Sprite onSprite;

    public void SetActive(bool active)
    {
        Sprite targetSprite = active ? onSprite : offSprite;

        foreach (var r in renderers)
        {
            r.sprite = targetSprite;
        }
    }
}