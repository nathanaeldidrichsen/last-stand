using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameStats", order = 3)]

public class GameStats : ScriptableObject
{
public int worldCoins;
public bool hasCompletedLevel1;
public bool hasCompletedLevel2;
public bool hasCompletedLevel3;
public bool hasCompletedLevel4;
public bool hasCompletedLevel5;
public int archerTier = 2;
public int cannonTier = 2;
public int fireTier = 2;
public int blessedTier = 2;
public int magicTier = 2;
public int frostTier = 2;
public int tierUpgradePrice = 30;

}
