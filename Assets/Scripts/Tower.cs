using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float shootRange = 10f; // Range within which the tower can shoot
    public int damage = 10; // Damage dealt by the tower
    public float shootSpeed = 1f; // Speed of the bullet
    public GameObject bullet; // Projectile prefab
    public float timeBetweenFire = 1f; // Time between each shot
    public Transform firePoint;

    private float fireCooldown; // Cooldown timer for shooting

    void Start()
    {
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
        // Instantiate the bullet
        GameObject spawnedBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = damage;

        // Calculate the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Get the bullet's rigidbody and set its velocity
        Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * shootSpeed;
        }

        // Optionally, set the damage on the bullet (requires a script on the bullet)
        Projectile projectileScript = spawnedBullet.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.damage = damage;
        }
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

    void OnDrawGizmosSelected()
    {
        // Draw the shoot range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}

// Example script for the bullet (attach this script to the bullet prefab)
public class Projectile : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deal damage to the enemy (requires an Enemy script with a TakeDamage method)
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
