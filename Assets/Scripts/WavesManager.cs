using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WavesManager : MonoBehaviour
{
    public Wave[] waves; // Array of waves containing enemy prefabs
    public float waitBetweenWaves = 10f; // Time to wait between waves
    public Transform spawnPoint;

    private int currentWaveIndex = 0; // Index to keep track of the current wave
    private bool isSpawning = false; // Flag to prevent multiple wave spawns at the same time

    private void Start()
    {
        StartFirstWave();
    }

    public void SpawnEnemiesFromWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            HUD.Instance.waveText.text = (currentWaveIndex + 1).ToString();
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.enemiesToSpawn.Length; i++)
        {
            Instantiate(wave.enemiesToSpawn[i], spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(1f); // Wait 1 second before spawning the next enemy
        }

        yield return new WaitForSeconds(waitBetweenWaves); // Wait for the specified time between waves
        isSpawning = false;
        StartNextWave();
    }

    // Method to start the first wave
    public void StartFirstWave()
    {
        if (!isSpawning && waves[0] != null)
        {
            currentWaveIndex = 0;
            SpawnEnemiesFromWave();
        }
    }

    public void StartNextWave()
    {
        if (currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            SpawnEnemiesFromWave();
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    // Optionally, you can add a method to start the first wave
    public void StartWaves()
    {
        if (!isSpawning)
        {
            currentWaveIndex = 0;
            SpawnEnemiesFromWave();
        }
    }
}

