using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static instance of GameManager which allows it to be accessed by any other script
    public int coins;
    public GameStats gameStats;
    [HideInInspector] public AudioSource audioSource; //A primary audioSource a large portion of game sounds are passed through
    public int currentLevel = 1;
    private static GameManager instance;
    private bool isPaused = false;
    public GameObject pausedText;


    // Public property to access the instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    Debug.LogError("No instance of GameManager found in the scene.");
                }
            }

            return instance;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pausedText.SetActive(isPaused);
        if (Input.GetKey(KeyCode.U))
        {
            KillAllEnemies();

        }
    }


    public void KillAllEnemies()
    {
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Enemy myEnemy = enemy.GetComponent<Enemy>();
                myEnemy.Die();
                // Destroy(enemy);
            }
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // If an instance already exists and it's not this, destroy this instance
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: to keep this instance between scene loads
        }
    }

    public void SpeedUpGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            HUD.Instance.speedBtnText.text = "SPEED x2";
        }
        else
        {
            HUD.Instance.speedBtnText.text = "SPEED x1";
            Time.timeScale = 1;
        }
    }

    public void LoadscenNme(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PauseGame()
    {
        if (isPaused)
        {
            // If the game is currently paused, resume the game
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            // If the game is not paused, pause the game
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    // Example method to demonstrate functionality
    public void StartGame()
    {
        Debug.Log("Game started!");
    }

    public void GetCoins(int coinAmount)
    {
        coins += coinAmount;
    }

    public void SpendCoins(int coinAmount)
    {
        coins -= coinAmount;
    }


}
