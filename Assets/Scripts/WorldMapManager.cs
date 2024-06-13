using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMapManager : MonoBehaviour
{
    public RectTransform playerUI; // The UI element representing the player
    public Animator playerUIAnim;
    public float moveSpeed = 5f; // Speed at which the player UI moves
    [SerializeField] private int clickedLevel = 0;
    private Vector2 targetPosition; // The target position for the player UI
    private bool isMoving = false; // Flag to check if the player UI is moving

    [Tooltip("Prefab of the dot to instantiate.")]
    public GameObject dotPrefab;

    public TMPro.TextMeshProUGUI coinsText;
    public GameStats gameStats;
    public string targetScene; // The name of the scene to load when reaching the target

    [Tooltip("Interval in seconds between each dot instantiation.")]
    public float dotInterval = 0.2f;
    private Coroutine trailCoroutine;
    private Animator anim;
    public Transform dotSpawnPosition;
    private Dictionary<string, System.Action> towerTierUpgrades;
    private static WorldMapManager instance;
    public static WorldMapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WorldMapManager>();

                if (instance == null)
                {
                    Debug.LogError("No instance of HUD found in the scene.");
                }
            }

            return instance;
        }
    }


    void Awake()
    {
        Time.timeScale = 1;
        towerTierUpgrades = new Dictionary<string, System.Action>
        {
            { "archer", () => gameStats.archerTier++ },
            { "cannon", () => gameStats.cannonTier++ },
            { "frost", () => gameStats.frostTier++ },
            { "blessed", () => gameStats.blessedTier++ },
            { "magic", () => gameStats.magicTier++ },
            { "fire", () => gameStats.fireTier++ }
        };
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        coinsText.text = gameStats.worldCoins.ToString();
        if (isMoving)
        {
            MoveToTarget();
        }
        playerUIAnim.SetBool("isMoving", isMoving);
    }


    // Method to check if the level is unlocked
    public bool CheckIfLevelUnlocked()
    {
        switch (clickedLevel)
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

    void MoveToTarget()
    {
        playerUI.position = Vector3.MoveTowards(playerUI.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(playerUI.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            if (trailCoroutine != null)
            {
                StopCoroutine(trailCoroutine);
                trailCoroutine = null;
            }
            anim.SetTrigger("FadeOut");
        }

        if (playerUI.position.x < targetPosition.x)
        {
            playerUI.localScale = new Vector3(-Mathf.Abs(playerUI.localScale.x), playerUI.localScale.y, playerUI.localScale.z);
        }
        else
        {
            playerUI.localScale = new Vector3(Mathf.Abs(playerUI.localScale.x), playerUI.localScale.y, playerUI.localScale.z);
        }
    }

    public void StartMovingToCastle(Transform castlePosition)
    {
        targetPosition = castlePosition.transform.position;

        if (CheckIfLevelUnlocked())
        {
            isMoving = true;

            if (trailCoroutine == null)
            {
                trailCoroutine = StartCoroutine(SpawnTrailDots());
            }
        }
        else
        {
            isMoving = false;
            Debug.Log("Level is locked.");
        }
    }

    public void SetSceneToLoadName(string sceneToLoad)
    {
        targetScene = sceneToLoad;
    }

    public void LoadScene()
    {
        if (targetScene != null)
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    public void SetClickedLevel(int lvl)
    {
        clickedLevel = lvl;
    }

    private IEnumerator SpawnTrailDots()
    {
        while (isMoving)
        {
            Instantiate(dotPrefab, dotSpawnPosition.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(dotInterval);
        }
    }

    public void UpgradeTowerTier(string towerName)
    {
        if (towerTierUpgrades.ContainsKey(towerName.ToLower()))
        {
            if (SpendWorldCoins(gameStats.tierUpgradePrice))
            {
                towerTierUpgrades[towerName.ToLower()]();
            }
        }
        else
        {
            Debug.LogWarning($"Tower type {towerName} does not exist.");
        }
    }

    public bool SpendWorldCoins(int amount)
    {
        if (gameStats.worldCoins >= amount)
        {
            gameStats.worldCoins -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough worldcoins!");
            return false;
        }
    }

}
