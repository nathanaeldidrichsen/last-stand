using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static instance of GameManager which allows it to be accessed by any other script
    private static GameManager instance;
    public int coins;

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
