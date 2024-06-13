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
                archerTier.text = WorldMapManager.Instance.gameStats.archerTier.ToString();
        cannonTier.text = WorldMapManager.Instance.gameStats.cannonTier.ToString();
        frostTier.text = WorldMapManager.Instance.gameStats.frostTier.ToString();
        fireTier.text = WorldMapManager.Instance.gameStats.fireTier.ToString();
        magicTier.text = WorldMapManager.Instance.gameStats.magicTier.ToString();
        blessedTier.text = WorldMapManager.Instance.gameStats.blessedTier.ToString();
    }
}
