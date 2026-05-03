using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHeal : MonoBehaviour
{
    public int maxCharge = 4;
    int currentCharge;

    public System.Action<int, int> OnChargeChanged;

    [Header("Refs")]
    public PlayerHealth playerHealth;

    [Header("Input")]
    public InputActionReference healAction;

    private AudioSource sourceHeal;
    [SerializeField] private AudioClip[] clipsHeal; 

    private void Start()
    {
        sourceHeal = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        healAction.action.Enable();
    }

    void OnDisable()
    {
        healAction.action.Disable();
    }

    void Update()
    {
        if (healAction.action.WasPressedThisFrame())
        {
            TryHeal();
        }
    }



    public void AddCharge(int value)
    {
        Debug.Log("HealDrop value: " + value);
        currentCharge = Mathf.Clamp(currentCharge + value, 0, maxCharge);
        OnChargeChanged?.Invoke(currentCharge, maxCharge);
    }


    void TryHeal()
    {
        if (currentCharge < maxCharge) return;
        if (playerHealth.CurrentHealth >= playerHealth.MaxHealth) return;

        playerHealth.Heal(2);
        PostProcessEffects.PlayHealEffect();
        sourceHeal.PlayOneShot(clipsHeal[Random.Range(0, clipsHeal.Length)]);
        currentCharge = 0;
        OnChargeChanged?.Invoke(currentCharge, maxCharge);
    }
}
