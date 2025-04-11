using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using TMPro;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject descriptionPanel;

    private InventoryItem selectedItem;
    private Dictionary<InventoryItem, Button> itemButtons = new Dictionary<InventoryItem, Button>();

    private void Start()
    {
        NewShopSave();
        LoadPurchasedItems(); // Load trạng thái mua từ PlayerPrefs
        PopulateShopGrid();
        shopManager.buyButton.onClick.AddListener(BuySelectedItem);
    }

    private void PopulateShopGrid()
    {
        foreach (InventoryItem item in shopManager.content)
        {
            GameObject slot = Instantiate(itemSlotPrefab, gridParent);
            Button button = slot.GetComponent<Button>();

            if (item == null)
            {
                slot.transform.Find("ItemImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
                continue;
            }
            else
            {
                slot.transform.Find("ItemImage").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slot.transform.Find("ItemImage").GetComponent<Image>().sprite = item.Icon;
            }

            if (button != null)
            {
                button.onClick.AddListener(() => SelectItem(item));
                itemButtons[item] = button;

                // Kiểm tra nếu item đã mua trước đó thì vô hiệu hóa slot
                if (PlayerPrefs.GetInt("PurchasedIn" + PlayerPrefs.GetString("PlayerID") + item.ItemID, 0) == 1)
                {
                    button.interactable = false;
                }
            }
        }
    }

    private void SelectItem(InventoryItem item)
    {
        if (itemButtons[item].interactable)
        {
            selectedItem = item;
            ShowItemDescription(item);
            shopManager.SetButton();
        }
    }

    private void BuySelectedItem()
    {
        if (selectedItem != null && itemButtons.ContainsKey(selectedItem))
        {
            shopManager.BuyItem(selectedItem);
            itemButtons[selectedItem].interactable = false; // Vô hiệu hóa slot sau khi mua
            PlayerPrefs.SetInt("PurchasedIn" + PlayerPrefs.GetString("PlayerID") + selectedItem.ItemID, 1); // Lưu trạng thái đã mua
            PlayerPrefs.Save();
            selectedItem = null;
        }
    }

    private void ShowItemDescription(InventoryItem item)
    {
        if (item != null)
        {
            descriptionPanel.SetActive(true);
            itemPrice.text = "Price: " + item.Price;
            itemImage.sprite = item.Icon;
            itemDescription.text = "Name: " + item.ItemName + item.Prefab.GetComponent<WeaponStats>().StatsToString();
        }
    }

    private void LoadPurchasedItems()
    {
        foreach (InventoryItem item in shopManager.content)
        {
            if (item != null && PlayerPrefs.GetInt("PurchasedIn" + PlayerPrefs.GetString("PlayerID") + item.ItemID, 0) == 1)
            {
                if (itemButtons.ContainsKey(item))
                {
                    itemButtons[item].interactable = false;
                }
            }
        }
    }

    private void NewShopSave()
    {
        foreach (InventoryItem item in shopManager.content)
        {
            if (PlayerPrefs.GetString("PlayerID") != "PlayerX00" && item != null &&
                PlayerPrefs.GetInt("PurchasedInPlayerX00" + item.ItemID, 0) == 1)
            {
                PlayerPrefs.SetInt("PurchasedIn" + PlayerPrefs.GetString("PlayerID") + selectedItem.ItemID, 1);
                PlayerPrefs.DeleteKey("PurchasedInPlayerX00" + selectedItem.ItemID);
            }
            PlayerPrefs.Save();
        }
    }
}