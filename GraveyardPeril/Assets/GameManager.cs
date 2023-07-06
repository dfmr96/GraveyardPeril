using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int money;
    public int remainingEnemies;
    public int wave;
    public GameObject[] wavePrefabs;
    public GameObject[] torches;
    public GameObject currentWave;
    public GameObject tutorialPanel;
    public GameObject shopPanel;
    public GameObject shopTutorialPanel;
    public GameObject shotgunPanel;
    public GameObject smgPanel;
    public Inventory playerInventory;
    public WeaponData shotgunData;
    public WeaponData smgData;
    public GameObject finalWavePanel;

    public GameObject pistolShop;
    public GameObject shotgunShop;
    public GameObject smgShop;

    public GameObject gameoverPanel;

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
        pistolShop.SetActive(true);
        if (wave > 0)
        {
            shotgunShop.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
        NextWave();
    }
    public void CloseShotgunPanel()
    {
        playerInventory.slots.Add(new Inventory.InventorySlot(shotgunData));
        for (int i = 0; i < torches.Length - 3; i++)
        {
            torches[i].SetActive(true);
        }
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

    public void OpenSMGPanel()
    {
        Time.timeScale = 0f;
        smgPanel.SetActive(true);
    }

    public void CloseSMGPanel()
    {
        playerInventory.slots.Add(new Inventory.InventorySlot(smgData));
        for (int i = 3; i < torches.Length; i++)
        {
            torches[i].SetActive(true);
        }
        smgPanel.SetActive(false);
        //shopTutorialPanel.SetActive(true);
        OpenShop();
    }

    public void WaveCleared()
    {
        switch (wave)
        {
            case 1:
                OpenShotgunPanel();
                break;
            case 2:
                OpenSMGPanel();
                break;
            case 3:
                FinalWavePanel();
                break;
        }
    }

    public void FinalWavePanel()
    {
//        Time.timeScale = 0f;
        finalWavePanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NextWave()
    {
        wave++;
        UIManager.sharedInstance.UpdateWaveText(wave);
        SpawnWave(wave);
    }
    
    public void AddMoney(int reward)
    {
        money += reward;
        UIManager.sharedInstance.UpdateMoney(money);
    }

    public void BuyHandgunAmmo()
    {
        if (money >= 200) 
        { 
        money -= 200;
        playerInventory.slots[0].totalAmmo = playerInventory.slots[0].maxAmmo;
            UIManager.sharedInstance.UpdateMoney(money);
        }
    }

    public void BuyShotgunAmmo()
    {
        if (money >= 400)
        {
            money -= 400;
            playerInventory.slots[1].totalAmmo = playerInventory.slots[1].maxAmmo;
            UIManager.sharedInstance.UpdateMoney(money);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
