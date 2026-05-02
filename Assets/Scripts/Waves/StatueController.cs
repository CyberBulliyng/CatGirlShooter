using UnityEngine;

public class StatueController : MonoBehaviour
{
    public SpriteRenderer[] renderers;

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