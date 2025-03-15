using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using UnityEngine.UIElements;

public class SavePoint : NPCController
{
    public InventoryDemoCharacter Player { get; private set; }
    [SerializeField] private GameObject saveLoadButton;
    
    protected void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Player = player.GetComponent<InventoryDemoCharacter>();
        }

        MMGameEvent.Trigger("Load");
        Debug.Log("NPC Game Manager Loaded Inventory");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && stayCheck)
            NPCFunction();
        else if(!stayCheck)
            saveLoadButton.SetActive(false);
    }

    public override void NPCFunction()
    {
        MMGameEvent.Trigger("Save");
        Debug.Log("NPC Game Manager Saved Inventory");
        saveLoadButton.SetActive(true);
    }
}
