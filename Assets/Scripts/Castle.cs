using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public GameObject sliderObject;
    public float displayTime = 3f;

    private int currentHealth;
    private bool isDisplayingSlider;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        UpdateHealthUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                TakeDamage(enemy.damage);
                other.GetComponent<Enemy>().Die();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        SoundManager.Instance.PlayCastleSound();
        currentHealth -= damage;
        UpdateHealthUI();

        // Display the health slider for a short duration
        if (!isDisplayingSlider)
        {
            isDisplayingSlider = true;
            sliderObject.SetActive(true);
            Invoke("HideHealthSlider", displayTime);
        }

        // Check if the castle has been destroyed
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        // Update the health slider value
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void HideHealthSlider()
    {
        isDisplayingSlider = false;
        sliderObject.SetActive(false);
    }

    void Die()
    {
        // Call the LostGame method from the HUD singleton
        if (HUD.Instance != null)
        {
            HUD.Instance.LostGame();
        }
        // Optionally, handle other game over logic here
    }
}
