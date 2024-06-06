using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapShop : MonoBehaviour
{
    public TMPro.TextMeshProUGUI archerTier;
    public TMPro.TextMeshProUGUI cannonTier;
    public TMPro.TextMeshProUGUI frostTier;
    public TMPro.TextMeshProUGUI fireTier;
    public TMPro.TextMeshProUGUI magicTier;
    public TMPro.TextMeshProUGUI blessedTier;

    void Start()
    {

    }

    void Update()
    {
                archerTier.text = WorldMapManager.Instance.gameStats.archerTowerTier.ToString();
        cannonTier.text = WorldMapManager.Instance.gameStats.cannonTowerTier.ToString();
        frostTier.text = WorldMapManager.Instance.gameStats.frostTowerTier.ToString();
        fireTier.text = WorldMapManager.Instance.gameStats.fireTowerTier.ToString();
        magicTier.text = WorldMapManager.Instance.gameStats.magicTowerTier.ToString();
        blessedTier.text = WorldMapManager.Instance.gameStats.blessedTowerTier.ToString();
    }
}
