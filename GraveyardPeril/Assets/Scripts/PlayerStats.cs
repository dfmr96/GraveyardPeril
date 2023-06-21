using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Weapon currentWeapon;
    public static event Action<PlayerStats> OnStatInit;
    public static event Action<float> OnPlayerDamaged;
    private void Awake()
    {
        health = maxHealth;
        OnStatInit?.Invoke(this);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        OnPlayerDamaged?.Invoke(damage);
    }
}
