using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource musicAudioSource;

    public AudioClip shootSound;
    public float shootVol;
    public AudioClip enemyHurtSound;
    public float enemyHurtVol;

    public AudioClip selectTowerSound;
    public float selectTowerVol;

    public AudioClip buildTowerSound;
    public float buildTowerVol;

    public AudioClip sellTowerSound;
    public float sellTowerVol;
        public AudioClip upgradeTowerSound;
    public float upgradeTowerVol;

        public AudioClip castleSound;
    public float castleVol;

            public AudioClip wonSound;
    public float wonVol;
                public AudioClip lostSound;
    public float lostVol = 0.3f;

                    public AudioClip worldMapUpgradeTowerSound;
    public float worldMapUpgradeTowerVol = 0.3f;




    [HideInInspector] public AudioSource audioSource; //A primary audioSource a large portion of game sounds are passed through
    private static SoundManager instance;

    // Public property to access the instance
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

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

    public void PlayUpgradeTower()
    {
        audioSource.PlayOneShot(upgradeTowerSound, upgradeTowerVol);
    }

    public void PlayWorldMapUpgradeTower()
    {
        audioSource.PlayOneShot(worldMapUpgradeTowerSound, worldMapUpgradeTowerVol);
    }
        public void PlayCastleSound()
    {
        audioSource.PlayOneShot(castleSound, castleVol);
    }

            public void PlayWonSound()
    {
        audioSource.PlayOneShot(wonSound, wonVol);
    }

                public void PlayLostSound()
    {
        musicAudioSource.enabled = false;
        audioSource.PlayOneShot(lostSound, lostVol);
    }


    public void PlaySellTowerSound()
    {
        audioSource.PlayOneShot(sellTowerSound, sellTowerVol);
    }

    public void PlayChooseTowerSound()
    {
        audioSource.PlayOneShot(selectTowerSound, selectTowerVol);
    }

        public void PlayBuildTowerSound()
    {
        audioSource.PlayOneShot(buildTowerSound, buildTowerVol);
    }

    public void PlayShootSound()
    {
        // audioSource.PlayOneShot(shootSound, shootVol);
    }

        public void PlayHurtSound()
    {
        audioSource.PlayOneShot(enemyHurtSound, enemyHurtVol);
    }
}
