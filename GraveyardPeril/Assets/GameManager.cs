using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int remainingEnemies;
    public int wave;
    public GameObject[] wavePrefabs;
    public GameObject[] torches;
    public GameObject currentWave;
    public GameObject tutorialPanel;
    public GameObject shopPanel;
    public GameObject shopTutorialPanel;
    public GameObject shotgunPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        wave = 1;
        SpawnWave(wave);
    }

    public void SpawnWave(int wave)
    {
        currentWave = Instantiate(wavePrefabs[wave - 1]);

        if (wave == 1)
        {
            Time.timeScale = 0;
            tutorialPanel.SetActive(true);
        }
    }

    public void HideTutorial()
    {
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false);
        UIManager.sharedInstance.UpdateWaveText(wave);
    }


    public void EnemySpawn()
    {
        remainingEnemies++;
    }
    public void EnemyDestroyed()
    {
        remainingEnemies--;

        if (remainingEnemies == 0)
        {
            Destroy(currentWave);
            WaveCleared();
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void CloseShotgunPanel()
    {
        shotgunPanel.SetActive(false);
        shopTutorialPanel.SetActive(true);
    }

    public void CloseShopTutorial()
    {
        shopTutorialPanel.SetActive(false);
        OpenShop();
    }

    public void OpenShotgunPanel()
    {
        Time.timeScale = 0f;
        shotgunPanel.SetActive(true);
    }

    public void WaveCleared()
    {
        switch (wave)
        {
            case 1:
                OpenShotgunPanel();
                break;
        }
    }

    public void NextWave()
    {
        wave++;
        UIManager.sharedInstance.UpdateWaveText(wave);
    }
}
