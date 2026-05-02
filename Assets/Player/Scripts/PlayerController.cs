using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Sorting")]
    public SpriteRenderer playerRenderer;
    public SpriteRenderer weaponRenderer;

    Rigidbody2D rb;
    Animator anim;
    Vector2 moveInput;
    Vector2 mouseDir;

    Vector2 knockbackVelocity;

    public InputActionReference moveAction;
    public InputActionReference lookAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
    }
    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>().normalized;

        // Поворот к курсору
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(lookAction.action.ReadValue<Vector2>());
        mouseDir = ((Vector2)mouseWorld - (Vector2)transform.position).normalized; 


        UpdateAnimations();
    }

    void FixedUpdate()
    {
        // Затухание отдачи
        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, 0.3f);
        rb.linearVelocity = moveInput * moveSpeed + knockbackVelocity;
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        knockbackVelocity = -direction.normalized * force;
    }

    void UpdateAnimations()
    {
        if (anim == null) return;

        bool moving = moveInput.sqrMagnitude > 0.01f;
        anim.SetBool("Moving", moving);

        // Для Blend Tree — передаём направление к курсору
        anim.SetFloat("XMove", mouseDir.x);
        anim.SetFloat("YMove", mouseDir.y);

        // Оружие поверх игрока только когда смотрим вниз
        if (weaponRenderer != null && playerRenderer != null)
        {
            bool lookingDown = mouseDir.y < -0.3f;
            weaponRenderer.sortingOrder = playerRenderer.sortingOrder + (lookingDown ? 1 : -1);
        }

    }

}