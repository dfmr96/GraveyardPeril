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
        public int weaponRange;
        public InventorySlot(WeaponData weaponData)
        {
            this.weaponData = weaponData;
            this.ammoInMagazine = weaponData.magazineSize;
            this.magazineSize = weaponData.magazineSize;
            this.totalAmmo = weaponData.maxAmmo;
            this.maxAmmo = weaponData.maxAmmo;
            this.weaponRange = weaponData.weaponRange;
        }
    }
    public List<InventorySlot> slots;
}
