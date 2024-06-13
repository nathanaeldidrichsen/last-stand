using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyStats", order = 3)]

public class EnemyStats : ScriptableObject
{

    public string enemyName = "Unknown";
    public float speed = 2f;
    public int health = 3;
    public int coinsToDrop;
    public bool hasRandomCoinDropAmount = false;
    public int damage = 1;
    public float recoveryTime = 1f;
    public string lore;
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
}
