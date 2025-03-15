using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class GetWeapon : NPCController
{
    [SerializeField] private WeaponItem weaponToGive;
    [SerializeField] private Inventory playerInventory;

    private void Update()
    {
        if (stayCheck && Input.GetKeyDown(KeyCode.Space))
        {
            NPCFunction();
        }
    }

    public override void NPCFunction()
    {
        if (playerInventory != null && weaponToGive != null)
        {
            bool added = playerInventory.AddItem(weaponToGive, 1);
            if (added)
            {
                Debug.Log("✅ Người chơi đã nhận vũ khí: " + weaponToGive.name);
            }
            else
            {
                Debug.LogWarning("⚠️ Không thể thêm vũ khí, Inventory có thể đã đầy!");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Chưa gán Inventory hoặc WeaponItem!");
        }
    }
}
