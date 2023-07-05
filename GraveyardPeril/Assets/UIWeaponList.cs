using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponList : MonoBehaviour
{
    [SerializeField] Inventory playerInventory;
    [SerializeField] GameObject weaponButtonPrefab;
    private void OnEnable()
    {
        foreach (Inventory.InventorySlot weaponSlot in playerInventory.slots)
        {
            GameObject newButton = Instantiate(weaponButtonPrefab, transform);
            newButton.GetComponent<WeaponButton>().weaponSlot = weaponSlot;
        }
    }

    private void OnDisable()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
