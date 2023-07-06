using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;
    [SerializeField] Image health;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject wavePanel;
    [SerializeField] TMP_Text waveText;
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
        wavePanel.SetActive(false);
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

    public void UpdateWaveText(int wave)
    {
        StartCoroutine(WaveBegin(wave));
    }

    public IEnumerator WaveBegin(int wave)
    {
        wavePanel.SetActive(true);
        waveText.SetText($"La Wave #{wave} ha comenzado");
        yield return new WaitForSeconds(2f);
        wavePanel.SetActive(false);
    }

    public void WaveCleared()
    {

    }
}
