using UnityEngine;
using UnityEngine.EventSystems;

public class PointerActionHandler : MonoBehaviour
{
    public GameObject statsUI; // The UI object to activate and set values on
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI weaknessText;
    public TMPro.TextMeshProUGUI enemyNameText;

    public TMPro.TextMeshProUGUI loreText;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            HandlePointerClick();
        }
    }

    void HandlePointerClick()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);

        foreach (RaycastHit2D hit in hits)
        {
            // Check if the hit object is on the "Enemy" layer
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Activate the UI object and set its values to the enemy's stats
                    statsUI.SetActive(true);
                    healthText.text =  enemy.health.ToString();
                    weaknessText.text = enemy.elementType.ToString();
                    loreText.text = enemy.lore.ToString();
                    enemyNameText.text = enemy.enemyName.ToString();
                    return; // Exit the method to avoid deactivating the UI
                }
            }
        }

        // Deactivate the UI object if no enemy is hit
        statsUI.SetActive(false);
    }
}
