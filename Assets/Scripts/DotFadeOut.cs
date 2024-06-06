using UnityEngine;
using UnityEngine.UI;

public class DotFadeOut : MonoBehaviour
{
    [Tooltip("The time in seconds for the dot to fully fade out.")]
    public float fadeDuration = 1f;

    private Image image;
    private float fadeTimer;

    void Start()
    {
        // Get the Image component
        image = GetComponent<Image>();

        // Initialize the timer
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        // Decrease the timer
        fadeTimer -= Time.deltaTime;

        // Calculate the new alpha value
        float alpha = fadeTimer / fadeDuration;

        // Update the color with the new alpha value
        if (image != null)
        {
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
        }

        // Destroy the game object when the timer reaches zero
        if (fadeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
