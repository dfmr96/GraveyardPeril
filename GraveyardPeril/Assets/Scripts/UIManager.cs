using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;
    [SerializeField] Image health;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject wavePanel;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text money;
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
        money.SetText("0");
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

    public void UpdateMoney(int reward)
    {
        money.SetText($"{reward}");
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
