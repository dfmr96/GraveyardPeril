using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponFireMode
{
    semi,
    auto
}

[CreateAssetMenu(menuName = "Data/New Weapon")]

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float magazineSize;
    public float maxAmmo;
    public Sprite sprite;
    public WeaponFireMode weaponFireMode;
    public GameObject weaponPrefab;

}
