
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int damage; // Damage dealt by the bullet
    [HideInInspector] public TowerData towerData; // Damage dealt by the bullet
    [HideInInspector] public bool isSlowdownBullet; // Damage dealt by the bullet
    [HideInInspector]public float slowdownSpeedAmount;
    [HideInInspector]public float slowdownDuration;
    public int fireDamage;
    public int coldDamge;
    public int lightDamge;



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlowdown(towerData.slowDownSpeedAmount);


                if(enemy.elementType == Enemy.ElementType.Grass && towerData.elementType == TowerData.ElementType.Fire)
                {
                    Debug.Log("Did extra fire damage");
                    damage = damage*4;
                }

                if(enemy.elementType == Enemy.ElementType.Magic && towerData.elementType == TowerData.ElementType.Magic)
                {
                    Debug.Log("Did extra Magic damage");
                    damage = damage*2;
                }

                if(enemy.elementType == Enemy.ElementType.Dark && towerData.elementType == TowerData.ElementType.Light)
                {
                    Debug.Log("Did extra Light damage");
                    
                    damage = damage*2;
                }
                if(enemy.elementType == Enemy.ElementType.Heavy && towerData.elementType == TowerData.ElementType.Heavy)
                {
                    Debug.Log("Did extra Heavy damage");

                    damage = damage*2;
                }

                SoundManager.Instance.PlayHurtSound();
                enemy.TakeDamage(damage);
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
        Destroy(gameObject, 1);
    }
}
