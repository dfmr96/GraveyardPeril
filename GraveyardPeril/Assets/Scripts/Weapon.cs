using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/New Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public float fireRate;
    public float magazineSize;
    public float maxAmmo;
    public Sprite sprite;
}
