using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variables for speed, health, and slowdown factor
    public float speed = 2f;
    public float speedDeviation = 0.1f;
    public int health = 3;
    public float slowdownFactor = 0.5f; // Set this to the fraction to slow down by (e.g., 0.5 for half speed)
    public int coinsToEarn;
    public int damage = 1;
    private SpriteRenderer spriteRenderer;

    // List of waypoints to move through
    public List<Transform> waypoints;

    // Private variables to track the current waypoint and original speed
    private int currentWaypointIndex = 0;
    private float originalSpeed;

    // Store the initial local scale for flipping
    private Vector3 initialLocalScale;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = Random.Range(speed - speedDeviation, speed);

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
        // Move towards the current waypoint
        MoveTowardsWaypoint();

        // Check if the enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.001f)
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

    // Function to move towards the current waypoint
    void MoveTowardsWaypoint()
    {
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Flip the localScale.x based on the direction of movement
        if (direction.x > 0)
        {
            /*transform.localScale = new Vector3(-Mathf.Abs(initialLocalScale.x), initialLocalScale.y, initialLocalScale.z);*/
            spriteRenderer.flipX = true;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = false;
            /*transform.localScale = new Vector3(Mathf.Abs(initialLocalScale.x), initialLocalScale.y, initialLocalScale.z);*/
        }
    }

    // Function to deal damage to the enemy
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.GetCoins(coinsToEarn);
            Destroy(gameObject);
        }
        else
        {
            // Apply the slowdown effect
            speed = originalSpeed * slowdownFactor;
        }
    }
}