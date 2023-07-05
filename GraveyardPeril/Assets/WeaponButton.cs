using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Inventory.InventorySlot weaponSlot;
    public WeaponData weaponData;
    public Image weaponSprite;

    private void Start()
    {
        weaponData = weaponSlot.weaponData;
        SetSprite();
    }

    public void SetSprite()
    {    
        weaponSprite.sprite = weaponData.sprite;
    }

    public void EquipWeapon()
    {
        PlayerInventory.OnWeaponEquipped?.Invoke(weaponSlot);
        Debug.Log("Weapon equipped");
    }
}
