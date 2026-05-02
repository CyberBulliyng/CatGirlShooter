using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    [Header("Sorting")]
    public SpriteRenderer playerRenderer;
    public SpriteRenderer thisRenderer;

    private void Start()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
        playerRenderer = PlayerController.instance.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if(transform.position.y < PlayerController.instance.transform.position.y)
        {
            thisRenderer.sortingOrder = playerRenderer.sortingOrder + 2;
        }
        else
        {
            thisRenderer.sortingOrder = playerRenderer.sortingOrder - 2;
        }
    }
}
