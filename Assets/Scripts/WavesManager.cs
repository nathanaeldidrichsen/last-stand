using System.Collections;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public Wave[] waves; // Array of waves containing enemy prefabs
    public float waitBetweenWaves = 4f; // Time to wait between waves
    public Transform spawnPoint;

    [SerializeField] private int currentWaveIndex = 0; // Index to keep track of the current wave
    private bool isSpawning = false; // Flag to prevent multiple wave spawns at the same time
    [SerializeField] private int activeEnemies = 0; // Counter to track active enemies

    private static WavesManager instance;
    
    // Public property to access the instance
    public static WavesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WavesManager>();
                if (instance == null)
                {
                    Debug.LogError("No instance of WavesManager found in the scene.");
                }
            }
            return instance;
        }
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

    private void Start()
    {
        StartCoroutine(WaitAndStartNextWave());
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        foreach (var enemy in wave.enemiesToSpawn)
        {
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            activeEnemies++;
            yield return new WaitForSeconds(1f); // Wait 1 second before spawning the next enemy
        }

        isSpawning = false;
    }

    public void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
            HUD.Instance.waveText.text = (currentWaveIndex).ToString();
        }
        else
        {
            Debug.Log("All waves completed!");
            HUD.Instance.WonGame();
        }
    }

    // Method to notify that an enemy has died
    public void OnEnemyDeath()
    {
        activeEnemies--;
        if (activeEnemies <= 0 && !isSpawning)
        {
            StartCoroutine(WaitAndStartNextWave());
        }
    }

    private IEnumerator WaitAndStartNextWave()
    {
        yield return new WaitForSeconds(waitBetweenWaves);
        StartNextWave();
    }
}
