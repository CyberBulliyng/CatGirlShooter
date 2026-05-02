using UnityEngine;
using UnityEngine.UI;

public class FountaineDecayUI : MonoBehaviour
{
    public Slider slider;
    public FountainController fountain;

    void Start()
    {
        if (fountain == null)
            fountain = FindObjectOfType<FountainController>();

        if (fountain != null)
        {
            fountain.onValueChanged.AddListener(UpdateUI);
            UpdateUI(fountain.GetProgress());
        }
        else
        {
            Debug.LogError("FountainController not found!");
        }
    }

    void UpdateUI(float value)
    {
        slider.value = value;
        slider.gameObject.SetActive(value > 0.01f);
    }
}