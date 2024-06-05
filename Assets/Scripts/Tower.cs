using UnityEngine;
using UnityEngine.UI;



public class Tower : MonoBehaviour
{
    public TowerData towerData;
    public int currentTier = 0;
    public TowerData[] upgradeList;
    [HideInInspector] public GameObject bullet; // Projectile prefab
    public float shootRange = 10f; // Range within which the tower can shoot
    public float shootSpeed; // Speed of the bullet
    private SpriteRenderer spriteRenderer;
    public int damage; // Damage dealt by the tower
    float timeBetweenFire; // Time between each shot
    int purchasePrice;
    string description;
    [Header("Slowdown effect")]
    public float slowdownSpeedAmount = 0;
    public float slowdownDuration = 0;
    public Button upgradeButton;
    public TMPro.TextMeshProUGUI infoText;
    public GameObject towerMenu; // The menu that appears when a tower is clicked
    public GameObject shootRangeObject;

    



    public Transform firePoint;

    private float fireCooldown; // Cooldown timer for shooting

    void Start()
    {
        towerData = upgradeList[0];
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetTowerStats();
        fireCooldown = 0f;
    }

    void Update()
    {
        // Reduce the cooldown timer
        fireCooldown -= Time.deltaTime;

        // Find the nearest enemy within shoot range
        GameObject targetEnemy = FindNearestEnemy();
        if (targetEnemy != null && fireCooldown <= 0f)
        {
            // Shoot at the enemy
            Shoot(targetEnemy.transform.position);
            fireCooldown = timeBetweenFire; // Reset the cooldown
        }
    }

    void Shoot(Vector3 targetPosition)
    {
        SoundManager.Instance.PlayShootSound();
        // Instantiate the bullet
        GameObject spawnedBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        spawnedBullet.GetComponent<Bullet>().damage = towerData.damage;
        spawnedBullet.GetComponent<Bullet>().towerData = towerData;

        spawnedBullet.GetComponent<Bullet>().slowdownSpeedAmount = towerData.slowDownSpeedAmount;
        spawnedBullet.GetComponent<Bullet>().slowdownDuration = towerData.slowDownDuration;

        // Calculate the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Get the bullet's rigidbody and set its velocity
        Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * shootSpeed;
        }

        // // Optionally, set the damage on the bullet (requires a script on the bullet)
        // Bullet projectileScript = spawnedBullet.GetComponent<Bullet>();
        // if (projectileScript != null)
        // {
        //     projectileScript.damage = damage;
        // }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= shootRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

        public void ShowMenu()
    {
        if (towerMenu.activeSelf)
        {
            towerMenu.SetActive(false);
        }
        else
        {
            ShowTowerStats();
            towerMenu.SetActive(true);
        }
    }

    public void ShowTowerStats()
    {
            SetTowerInfoText("Damage: " + towerData.damage + "\n" + "APS: " + towerData.timeBetweenFire + "\n" + "Range: " + towerData.shootRange + "\n");
    }

    public void SellTower()
    {
        SoundManager.Instance.PlaySellTowerSound();
        GameManager.Instance.coins += towerData.sellPrice;
        Destroy(this.gameObject);
    }

    public void SetTowerStats()
    {
        bullet = towerData.bullet;
        shootSpeed = towerData.shootSpeed;
        damage = towerData.damage;
        timeBetweenFire = towerData.timeBetweenFire;
        purchasePrice = towerData.purchasePrice;
        spriteRenderer.sprite = towerData.towerSprite;
        description = towerData.description;
        slowdownDuration = towerData.slowDownDuration;
        slowdownSpeedAmount = towerData.slowDownSpeedAmount;
        shootRange = towerData.shootRange;
    if (shootRangeObject != null)
    {
        shootRangeObject.transform.localScale = new Vector2(shootRange *2, shootRange*2);
    }
    }

    public void UpgradeTower()
    {
        // ShowMenu();
        if (currentTier < upgradeList.Length + 1)
        {
            if (upgradeList[currentTier].upgradePrice < GameManager.Instance.coins)
            {
                SoundManager.Instance.PlayBuildTowerSound();
                GameManager.Instance.SpendCoins(towerData.upgradePrice);
                currentTier++;
                towerData = upgradeList[currentTier];
                SetTowerStats();
            }
        }
    }

        // Method to display the shoot range in the game view
public void DisplayShootRange()
{
    // Toggle the active state of shootRangeObject
    shootRangeObject.SetActive(!shootRangeObject.activeSelf);
}
    public void SellTowerInfo()
    {
        infoText.text = "Sell tower for " + towerData.sellPrice + " coins";
    }

    public void CheckIfUpgradeable(string text)
    {
        if (currentTier >= upgradeList.Length - 1)
        {
        infoText.text = "Tower is fully upgraded";
        }
        else if (upgradeList[currentTier].upgradePrice > GameManager.Instance.coins)
        {
        infoText.text = "Cost " + towerData.upgradePrice + "\n" + "Insufficient coins";

        }
        else
        {
            infoText.text = "Cost " + towerData.upgradePrice + "\n" + text;
        }
    }

    public void SetTowerInfoText(string text)
    {
        infoText.text = text;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the shoot range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
