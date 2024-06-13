using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public GameStats gameStats;

    [SerializeField] private string whichScene;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        ResetGameStats();
        UnityEngine.SceneManagement.SceneManager.LoadScene(whichScene);
    }

    public void ResetGameStats()
    {
        gameStats.worldCoins = 0;
        gameStats.hasCompletedLevel1 = false;
        gameStats.hasCompletedLevel2 = false;
        gameStats.hasCompletedLevel3 = false;
        gameStats.hasCompletedLevel4 = false;
        gameStats.hasCompletedLevel5 = false;
        gameStats.archerTier = 1;
        gameStats.cannonTier = 1;
        gameStats.fireTier = 1;
        gameStats.blessedTier = 1;
        gameStats.magicTier = 1;
        gameStats.frostTier = 1;
    }
}
