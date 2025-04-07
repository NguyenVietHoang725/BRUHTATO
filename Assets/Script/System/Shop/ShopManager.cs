using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public InventoryItem[] content;
    [SerializeField] private Inventory coinInventory;
    [SerializeField] private Inventory mainInventory;
    public Button buyButton;

    public void SetButton()
    {
        if(coinInventory.Content[0] != null)
        {
            foreach (InventoryItem item in content)
            {
                if (item != null)
                {
                    if (coinInventory.Content[0].Quantity >= item.Price)
                    {
                        buyButton.interactable = true;
                    }
                    else
                    {
                        buyButton.interactable = false;
                    }
                }
            }
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void BuyItem(InventoryItem item)
    {
        if(item != null)
        {
            coinInventory.Content[0].Quantity -= item.Price;
            mainInventory.AddItem(item, 1);
        }
    }
}
