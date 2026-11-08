using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fillImage; // Ensure your child 'Fill' object is assigned here

    [Header("Hydration Stats")]
    [SerializeField] private float maxHydration = 100f;
    [SerializeField] private float currentHydration = 100f;

    public float CurrentHydration 
    {
        get => currentHydration;
        set 
        {
            currentHydration = Mathf.Clamp(value, 0f, maxHydration);
            UpdateHydrationBar();
        }
    }

    private void Start()
    {
        UpdateHydrationBar();
    }

    // This runs automatically in the editor whenever you change a value in the Inspector
    private void OnValidate()
    {
        currentHydration = Mathf.Clamp(currentHydration, 0f, maxHydration);
        UpdateHydrationBar();
    }

    private void UpdateHydrationBar()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentHydration / maxHydration;
        }
    }

    public void ModifyHydration(float amount)
    {
        CurrentHydration += amount;
    }
}