using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : NPCController
{
    [SerializeField] GameObject shopPanel;

    private void Update()
    {
        NPCFunction();
    }

    public override void NPCFunction()
    {
        if (Input.GetButtonDown("Interact") && stayCheck)
        {
            shopPanel.SetActive(true);
        }
        if(!stayCheck) shopPanel.SetActive(false);
    }
}