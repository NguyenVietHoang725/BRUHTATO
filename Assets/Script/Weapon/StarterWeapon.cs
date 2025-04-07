using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;

public class StarterWeapon : MonoBehaviour
{
    [SerializeField] private Inventory weaponInventory;

    [SerializeField] private InventoryItem starterWeapon;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("WeaponInventorySave" + PlayerPrefs.GetString("PlayerID")))
            weaponInventory.AddItem(starterWeapon, 1);
    }
}
