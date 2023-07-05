using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;
    public Inventory.InventorySlot currentWeaponSlot;
    public GameObject weaponParent;
    public GameObject currentWeapon;
    public static Action<Inventory.InventorySlot> OnWeaponEquipped;
    public PlayerController playerController;
    public WeaponData handgun;
    private void Start()
    {
        inventory.slots.Clear();
        inventory.slots.Add(new Inventory.InventorySlot(handgun, handgun.magazineSize, handgun.magazineSize, handgun.maxAmmo, handgun.maxAmmo));
        EquipWeapon(inventory.slots[0]);
    }

    private void OnEnable()
    {
        OnWeaponEquipped += EquipWeapon;
    }

    public void EquipWeapon (Inventory.InventorySlot slot)
    {
        currentWeaponSlot = slot;
        int children = weaponParent.transform.childCount;

        for (int i = 0; i < children; i++)
        {
            Destroy(weaponParent.transform.GetChild(i).gameObject);
        }
        currentWeapon = Instantiate(currentWeaponSlot.weaponData.weaponPrefab, weaponParent.transform);
        playerController.SetWeapon(currentWeaponSlot.weaponData, currentWeapon);
    }
}
