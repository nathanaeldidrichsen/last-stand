using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public TMPro.TextMeshProUGUI waveText;
    public TMPro.TextMeshProUGUI coinsText;
    public GameObject deadMenu;
    public GameObject shopMenu;
    private bool isShopOpen;


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
        deadMenu.SetActive(true);
        Time.timeScale = 0.01f;
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
        SceneManager.LoadScene(currentSceneIndex);
    }
}
