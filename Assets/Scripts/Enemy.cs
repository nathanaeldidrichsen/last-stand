

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Public variables for speed, health, and slowdown factor
    public float speed = 2f;
    public int health = 3;
    public int coinsToDrop;
    public bool hasRandomCoinDropAmount = false;
    public int damage = 1;
    private int currentHealth;
    private SpriteRenderer sprite;
    private Animator anim;

    public Slider healthSlider;

    // List of waypoints to move through
    public List<Transform> waypoints;

    // Private variables to track the current waypoint and original speed
    private int currentWaypointIndex = 0;
    [SerializeField] private float originalSpeed;

    // Store the initial local scale for flipping
    private Vector3 initialLocalScale;
    private bool isDead;
    [SerializeField] private RecoveryCounter recoveryCounter;
    public ElementType elementType = ElementType.Normal;


        public enum ElementType
{
    Fire,
    Water,
    Ice,
    Grass,
    Light,
    Heavy,
    Normal,
    Magic,
    Lightning,
    Dark
}

    // Start is called before the first frame update
    void Start()
    {

        recoveryCounter = GetComponent<RecoveryCounter>();
        if(hasRandomCoinDropAmount)
        {
            coinsToDrop = Random.Range(1, coinsToDrop); 
        }

        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        currentHealth = health;
        healthSlider.maxValue = health;
        UpdateHealthUI();


        // Ensure there are waypoints in the list
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints set for the enemy.");
            return;
        }

        // Set the enemy's position to the first waypoint
        transform.position = waypoints[currentWaypointIndex].position;

        // Store the original speed and initial local scale
        originalSpeed = speed;
        initialLocalScale = transform.localScale;
    }



    // Update is called once per frame
    void Update()
    {

        if(!recoveryCounter.recovering)
        {
            speed = originalSpeed;
        }
        // Move towards the current waypoint
        MoveTowardsWaypoint();

        // Check if the enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.01f)
        {
            // Move to the next waypoint if available
            if (currentWaypointIndex < waypoints.Count - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                // If it's the last waypoint, destroy the enemy or handle it as needed
                // For example, you might want to remove health from the player
                Destroy(gameObject);
            }
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
    // Function to move towards the current waypoint
    void MoveTowardsWaypoint()
    {

        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Flip the localScale.x based on the direction of movement
        if (direction.x > 0)
        {
            sprite.flipX = true;
            // transform.localScale = new Vector3(-Mathf.Abs(initialLocalScale.x), initialLocalScale.y, initialLocalScale.z);
        }
        else if (direction.x < 0)
        {
            sprite.flipX = false;
            // transform.localScale = new Vector3(Mathf.Abs(initialLocalScale.x), initialLocalScale.y, initialLocalScale.z);
        }
    }

    // Function to deal damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameManager.Instance.GetCoins(coinsToDrop);
            Die();
        }
    }

        public void TakeElementalDamage(int fireDmg, int coldDmg, int lightDmg)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameManager.Instance.GetCoins(coinsToDrop);
            Die();
        }
    }

    public void Die()
    {
        if(!isDead)
        {
        isDead = true;
        WavesManager.Instance.OnEnemyDeath();
        Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void ApplySlowdown(float slowdownSpeedAmount)
    {
        if(slowdownSpeedAmount > 0)
        {
        speed = 0.1f;
        recoveryCounter.Recover();
        }
        // if (!recoveryCounter.recovering)
        // {
        //     recoveryCounter.Recover();
        //     if (speed > slowdownSpeedAmount)
        //     {
        //         speed = speed - slowdownSpeedAmount;
        //     }
        //     else
        //     {
        //         speed = 0.05f;
        //     }
        // }
    }
}