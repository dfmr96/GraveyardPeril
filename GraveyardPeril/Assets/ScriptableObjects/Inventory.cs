using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "New Inventory")]
public class Inventory : ScriptableObject

{
    [Serializable]
    public class InventorySlot
    {
        public WeaponData weaponData;
        public float ammoInMagazine;
        public float magazineSize;
        public float totalAmmo;
        public float maxAmmo;

        public InventorySlot(WeaponData weaponData, float ammoInMagazine, float magazineSize, float totalAmmo, float maxAmmo)
        {
            this.weaponData = weaponData;
            this.ammoInMagazine = ammoInMagazine;
            this.magazineSize = magazineSize;
            this.totalAmmo = totalAmmo;
            this.maxAmmo = maxAmmo;
        }
    }
    public List<InventorySlot> slots;
}
