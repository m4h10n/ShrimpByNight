using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Drag your UI Image GameObject here.")]
    public GameObject victoryImage;

    [Header("Settings")]
    [Tooltip("Time in seconds the player needs to survive.")]
    public float survivalTimeRequired = 60f;

    private float timer = 0f;
    private bool hasSurvived = false;

    void Start()
    {
        // Make sure the image is hidden when the game starts
        if (victoryImage != null)
        {
            victoryImage.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Victory Image reference is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        // Stop counting once they have already survived
        if (hasSurvived) return;

        // Count up time
        timer += Time.deltaTime;

        // Check if 60 seconds have passed
        if (timer >= survivalTimeRequired)
        {
            TriggerSurvival();
        }
    }

    void TriggerSurvival()
    {
        hasSurvived = true;

        // Show the PNG image
        if (victoryImage != null)
        {
            victoryImage.SetActive(true);
        }

        // Optional: Uncomment below if you want to pause the game when they win
        // Time.timeScale = 0f;
        
        Debug.Log("Player survived for 60 seconds!");
    }
}