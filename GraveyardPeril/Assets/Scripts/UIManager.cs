using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;
    [SerializeField] Image health;
    [SerializeField] PlayerStats playerStats;
    private void Start()
    {
        if (sharedInstance != null)
        {
            Destroy(gameObject);
        } else
        {
            sharedInstance = this;
        }

        SetHealth();
    }

    private void OnEnable()
    {
        PlayerStats.OnStatInit += SetPlayerStats;
        PlayerStats.OnPlayerDamaged += UpdateHealth;
    }

    private void OnDisable()
    {
        PlayerStats.OnStatInit -= SetPlayerStats;
        PlayerStats.OnPlayerDamaged -= UpdateHealth;
    }

    public void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }

    public void SetHealth()
    {
        health.fillAmount = playerStats.health / playerStats.maxHealth;
    }

    public void UpdateHealth(float damage)
    {
        health.fillAmount -= damage / playerStats.maxHealth;
    }
}
