using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerData", order = 2)]

public class TowerData : ScriptableObject
{
    public GameObject bullet; // Projectile prefab
    public int damage = 10; // Damage dealt by the tower
    public int purchasePrice = 10;
    public int sellPrice = 5;
    public float shootRange = 10f; // Range within which the tower can shoot

    public int upgradePrice = 5;
    public float shootSpeed = 1f; // Speed of the bullet
    public float timeBetweenFire = 1f; // Time between each shot
    public string description ="Basic cannon tower does single target damage";
    public int fireDamage;
    public int coldDamge;
    public int lightDamge;

    public string towerName;
            public float slowDownDuration;
        public float slowDownSpeedAmount;
    public Sprite towerSprite;
    public ElementType elementType;
    public enum ElementType
{
    Fire,
    Ice,
    Grass,
    Light,
    Heavy,
    Normal,
    Magic,
    Water,
    Lightning,
    Dark
}

}
