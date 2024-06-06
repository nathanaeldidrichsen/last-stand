using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapCastle : MonoBehaviour
{
    public GameStats gameStats;
    public Image castleImage;
    public int level;
    public bool isUnlocked;
    private void Start()
    {
        castleImage = GetComponent<Image>();
        isUnlocked = SetLevelLocked();

        if (isUnlocked)
        {
            castleImage.color = Color.white; // Set color to white if unlocked
        }
        else
        {
            castleImage.color = Color.black; // Set color to black if locked
        }
    }

    public bool SetLevelLocked()
    {
        switch (level)
        {
            case 1:
                return true; // Level 1 is always unlocked
            case 2:
                return gameStats.hasCompletedLevel1;
            case 3:
                return gameStats.hasCompletedLevel2;
            case 4:
                return gameStats.hasCompletedLevel3;
            case 5:
                return gameStats.hasCompletedLevel4;
            default:
                return false; // If the level is not between 1-5, it's locked
        }
    }
}
