using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject towerPrefab; // Tower prefab to be instantiated
    public GameObject placementIndicatorPrefab; // Prefab for the placement indicator
        private SpriteRenderer placementIndicatorRenderer; // SpriteRenderer of the placement indicator

    private GameObject currentTower; // Instance of the tower being placed
    private GameObject placementIndicator; // Instance of the placement indicator
    public LayerMask groundLayer; // Layer mask for ground layer
    private Image image;
    private Color originalColor;
    public Color hoverColor = new Color(0.8f, 0.8f, 0.8f); // Slightly darker color
    public bool canBePlaced;
        public Color canPlaceColor = Color.green; // Color when the tower can be placed
    public Color cannotPlaceColor = Color.red; // Color when the tower cannot be placed
    private float gridSize = 0.16f; // Size of the grid
    private float towerHeightPlacementOffset = 0.08f; // Offset to adjust the tower position
    
    
    private float xOffset = -0.08f; // Offset on the x-axis
    private float yOffset = -0.23f; // Offset on the y-axis

    Tower tower;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        if (towerPrefab != null)
        {
            tower = towerPrefab.GetComponent<Tower>();
        }
    }

    // Method to cancel the tower purchase
    public void CancelPurchase()
    {
        if (currentTower != null)
        {
            Destroy(currentTower); // Destroy the tower instance
            currentTower = null; // Clear the current tower
        }
        if (placementIndicator != null)
        {
            Destroy(placementIndicator); // Destroy the placement indicator
            placementIndicator = null; // Clear the placement indicator
        }
    }

    // Show description when pointer enters
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tower.towerData.description != null)
        {
            SoundManager.Instance.PlayChooseTowerSound();
            HUD.Instance.towerNameText.text = tower.towerData.towerName.ToString();
            HUD.Instance.towerDescriptionText.text = tower.towerData.description;
            HUD.Instance.towerDamageText.text = tower.towerData.damage.ToString();
            HUD.Instance.towerSpeedText.text = tower.towerData.timeBetweenFire.ToString();
            HUD.Instance.towerCostText.text = tower.towerData.purchasePrice.ToString();
        }
        image.color = hoverColor;
    }

    // Hide description when pointer exits
    public void OnPointerExit(PointerEventData eventData)
    {
        if (tower.towerData.description != null)
        {
            HUD.Instance.towerDescriptionText.text = "";
            HUD.Instance.towerDamageText.text = "";
            HUD.Instance.towerSpeedText.text = "";
            HUD.Instance.towerCostText.text = "";
        }
        image.color = originalColor;
    }

    // Called when the pointer is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentTower == null && towerPrefab != null)
        {
            placementIndicator = Instantiate(placementIndicatorPrefab);
            placementIndicatorRenderer = placementIndicator.GetComponent<SpriteRenderer>();

            Debug.Log("Placement indicator instantiated.");

            // Update placement indicator position to follow the mouse position
            UpdatePlacementIndicatorPosition();
        }
    }

    // Called when dragging the pointer
    public void OnDrag(PointerEventData eventData)
    {
        HUD.Instance.anim.SetBool("isDragging", true);
        if (placementIndicator != null)
        {
            // Update placement indicator position to follow the mouse position
            UpdatePlacementIndicatorPosition();
        }
    }

    // Called when the pointer is released
public void OnPointerUp(PointerEventData eventData)
{
        HUD.Instance.anim.SetBool("isDragging", false);

    if (placementIndicator != null)
    {
        // Get all objects hit by the raycast
        RaycastHit2D[] hits = Physics2D.RaycastAll(placementIndicator.transform.position, Vector2.zero, Mathf.Infinity);

        bool hitGround = false;
        bool hitRoad = false;

        // Iterate through all hit objects
        foreach (var hit in hits)
        {
            string layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            Debug.Log("Hit layer: " + layerName);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Road") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                hitRoad = true;
                break; // Stop if we hit the road
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                hitGround = true;
            }
        }

        // Decide if the tower can be placed
        if (hitRoad)
        {
            // If we hit the road, cancel the purchase
            Debug.Log("Cannot place tower here: hit a road layer.");
            CancelPurchase();
        }
        else if (hitGround && GameManager.Instance.coins >= tower.towerData.purchasePrice)
        {
            // If we hit the ground and have enough coins, place the tower
            GameManager.Instance.SpendCoins(tower.towerData.purchasePrice);
            SoundManager.Instance.PlayBuildTowerSound();

            Debug.Log("Coins spent, tower placed.");

            // Calculate the final position with offset
            Vector3 finalPosition = placementIndicator.transform.position;
            finalPosition.y -= towerHeightPlacementOffset;

            // Instantiate the tower prefab at the placement indicator position
            currentTower = Instantiate(towerPrefab, finalPosition, Quaternion.identity);

            GameObject world = GameObject.FindGameObjectWithTag("World");
            if (world != null)
            {
                currentTower.transform.SetParent(world.transform, true);
            }

            // Clear the current tower for the next purchase
            currentTower = null;

            // Destroy the placement indicator
            Destroy(placementIndicator);
            placementIndicator = null;
        }
        else
        {
            // Cancel the purchase if not placed on ground layer or not enough coins
            Debug.Log("Cannot place tower here: no valid ground layer hit or not enough coins.");
            CancelPurchase();
        }
    }
}


    // Method to snap a position to the nearest grid point
    private Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round((position.x - xOffset) / gridSize) * gridSize + xOffset;
        float y = Mathf.Round((position.y - yOffset) / gridSize) * gridSize + yOffset;
        return new Vector3(x, y, position.z);
    }

    // Update the position of the placement indicator to follow the mouse cursor
    private void UpdatePlacementIndicatorPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        placementIndicator.transform.position = SnapToGrid(mousePos);

        // Update the color of the placement indicator
        if (CanPlaceTower(placementIndicator.transform.position))
        {
            placementIndicatorRenderer.color = canPlaceColor;
        }
        else
        {
            placementIndicatorRenderer.color = cannotPlaceColor;
        }
    }

        // Method to check if the tower can be placed at the given position
    private bool CanPlaceTower(Vector3 position)
    {
        // Check if the placement indicator is over the ground layer and not the road layer
        RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero, Mathf.Infinity);

        bool canPlace = false;

        foreach (var hit in hits)
        {
            string layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            Debug.Log("Hit layer: " + layerName);

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Road") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                canPlace = false;
                break;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                canPlace = true;
            }
        }

        return canPlace && GameManager.Instance.coins >= tower.towerData.purchasePrice;
    }
}
