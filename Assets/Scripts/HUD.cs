
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{

    public TMPro.TextMeshProUGUI waveText;
    public TMPro.TextMeshProUGUI coinsText;
    public TMPro.TextMeshProUGUI towerDescriptionText;
    public TMPro.TextMeshProUGUI towerDamageText;
    public TMPro.TextMeshProUGUI towerNameText;

    public TMPro.TextMeshProUGUI towerSpeedText;
    public TMPro.TextMeshProUGUI speedBtnText;

    public TMPro.TextMeshProUGUI towerCostText;




    public GameObject deadMenu;
    public GameObject wonMenu;

    public GameObject shopMenu;
    private bool isShopOpen;
    [HideInInspector] public Animator anim;

    private static HUD instance;

    // Public property to access the instance
    public static HUD Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HUD>();

                if (instance == null)
                {
                    Debug.LogError("No instance of HUD found in the scene.");
                }
            }

            return instance;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        coinsText.text = GameManager.Instance.coins.ToString();
    }

    public void LostGame()
    {
        SoundManager.Instance.PlayLostSound();
        deadMenu.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void WonGame()
    {
        SoundManager.Instance.PlayWonSound();
        wonMenu.SetActive(true);
    }

    public void OpenShop()
    {
        if (!isShopOpen)
        {
            isShopOpen = true;
            shopMenu.SetActive(true);
        }
        else
        {
            isShopOpen = false;
            shopMenu.SetActive(false);
        }
    }

    public void Retry()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
