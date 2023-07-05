using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAmmoController : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;
    [SerializeField] TMP_Text magazine;
    [SerializeField] TMP_Text ammo;

    public static Action<Inventory.InventorySlot> OnAmmoValueChanged;

    private void OnEnable()
    {
        OnAmmoValueChanged += SetAmmo;
    }

    private void OnDisable()
    {
        OnAmmoValueChanged -= SetAmmo;
    }

    public void SetAmmo(Inventory.InventorySlot slot)
    {
        magazine.SetText($"{slot.ammoInMagazine}/{slot.magazineSize}");
        ammo.SetText($"{slot.totalAmmo}/{slot.maxAmmo}");
    }
}
