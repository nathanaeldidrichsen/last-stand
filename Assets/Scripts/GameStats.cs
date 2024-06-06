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
public int archerTowerTier = 2;
public int cannonTowerTier = 2;
public int fireTowerTier = 2;
public int blessedTowerTier = 2;
public int magicTowerTier = 2;
public int frostTowerTier = 2;
public int tierUpgradePrice = 30;

}
