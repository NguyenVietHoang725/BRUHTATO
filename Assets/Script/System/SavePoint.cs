using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;

public class SavePoint : NPCController
{
    public InventoryDemoCharacter Player { get; private set; }

    protected void Start()
    {
        // Tìm người chơi theo tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Player = player.GetComponent<InventoryDemoCharacter>();
        }

        // Kích hoạt sự kiện Load để nạp dữ liệu
        MMGameEvent.Trigger("Load");
        Debug.Log("NPC Game Manager Loaded Inventory");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && stayCheck)
            NPCFunction();
    }

    public override void NPCFunction()
    {
        MMGameEvent.Trigger("Save");
        Debug.Log("NPC Game Manager Saved Inventory");
    }

}
