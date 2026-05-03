using UnityEngine;
using UnityEngine.Events;

public class FountainController : MonoBehaviour
{
    public float maxValue = 100f;
    public float currentValue = 0f;

    public float fillPerEnemy = 1f;
    public float decaySpeed = 4f; 

    int enemiesInZone = 0;

    public UnityEvent onLose;
    public UnityEvent<float> onValueChanged; 

    void Update()
    {
        if (enemiesInZone > 0)
        {
            currentValue += fillPerEnemy * enemiesInZone *0.4f * Time.deltaTime;
        }
        else
        {
            currentValue -= decaySpeed * Time.deltaTime;
        }

        currentValue = Mathf.Clamp(currentValue, 0, maxValue);

        onValueChanged?.Invoke(currentValue / maxValue);

        if (currentValue >= maxValue)
        {
            onLose?.Invoke();
        }
    }

    public float GetProgress()
    {
        return currentValue / maxValue;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInZone++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInZone = Mathf.Max(0, enemiesInZone - 1);
    }
}