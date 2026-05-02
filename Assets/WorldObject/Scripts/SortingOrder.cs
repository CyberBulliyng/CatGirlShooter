using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    [Header("Sorting")]
    public SpriteRenderer playerRenderer;
    public SpriteRenderer thisRenderer;

    [Header("Mode")]
    public bool usePlayerLogic = true;

    public int offset = 0;

    private void Start()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
        if (PlayerController.instance != null)
            playerRenderer = PlayerController.instance.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (thisRenderer == null) return;

        if (usePlayerLogic && playerRenderer != null)
        {
            if (transform.position.y < PlayerController.instance.transform.position.y)
            {
                thisRenderer.sortingOrder = playerRenderer.sortingOrder + 2 + offset;
            }
            else
            {
                thisRenderer.sortingOrder = playerRenderer.sortingOrder - 2 + offset;
            }
        }
        else
        {
            thisRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + offset;
        }
    }
}
