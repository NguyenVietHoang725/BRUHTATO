using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.InventoryEngine;
using UnityEngine;
using UnityEngine.UI;

public class TradeGemCoin : MonoBehaviour
{
    [SerializeField] private InventoryItem coin, gem;
    [SerializeField] private Inventory mainInventory, coinInventory;
    [SerializeField] private int coinFees, gemFees;
    [SerializeField] private Button gemToCoinButton, coinToGemButton;
    
    private int gemItemIndex = -1;

    private void Start()
    {
        for(int i = 0; i < mainInventory.Content.Length; i++)
        {
            if (mainInventory.Content[i] == gem)
            {
                gemItemIndex = i;
                break;
            }
        }
    }

    private void OnEnable()
    {
        CanTrade();
    }

    private void CanTrade()
    {
        if (gemItemIndex == -1 || mainInventory.Content[gemItemIndex].Quantity <= gemFees) 
            gemToCoinButton.interactable = false;
        else gemToCoinButton.interactable = true;
        
        if (coinInventory.Content[0] == null || coinInventory.Content[0].Quantity <= coinFees) coinToGemButton.interactable = false;
        else coinToGemButton.interactable = true;
    }

    public void GemToCoin()
    {
        mainInventory.Content[gemItemIndex].Quantity -= gemFees;
        if(coinInventory.Content[0] == null) coinInventory.AddItem(coin, coinFees);
        else coinInventory.Content[0].Quantity += coinFees;
    }

    public void CoinToGem()
    {
        coinInventory.Content[0].Quantity -= coinFees;
        if(gemItemIndex == -1) mainInventory.AddItem(gem, gemFees);
        else mainInventory.Content[gemItemIndex].Quantity += gemFees;
    }
}