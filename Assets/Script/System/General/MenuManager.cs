using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button syncButton;
    
    public void StoryMode()
    {
        return;
    }

    public void EndlessMode()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void Start()
    {
        NewSaveID();
        if(!PlayerPrefs.HasKey("MainInventorySave" + PlayerPrefs.GetString("PlayerID")) || PlayerPrefs.GetString("PlayerID") == "PlayerX00")
            syncButton.interactable = false;
    }

    private void NewSaveID()
    {
        string playerID = PlayerPrefs.GetString("PlayerID");
        if (playerID != "PlayerX00" && PlayerPrefs.HasKey("MainInventorySavePlayerX00")
                                    && PlayerPrefs.HasKey("WeaponInventorySavePlayerX00")
                                    && PlayerPrefs.HasKey("CoinInventorySavePlayerX00"))
        {
            PlayerPrefs.SetString("MainInventorySave" + playerID, PlayerPrefs.GetString("MainInventorySavePlayerX00"));
            PlayerPrefs.SetString("WeaponInventorySave" + playerID, PlayerPrefs.GetString("WeaponInventorySavePlayerX00"));
            PlayerPrefs.SetString("CoinInventorySave" + playerID, PlayerPrefs.GetString("CoinInventorySavePlayerX00"));
            
            PlayerPrefs.DeleteKey("MainInventorySavePlayerX00");
            PlayerPrefs.DeleteKey("WeaponInventorySavePlayerX00");
            PlayerPrefs.DeleteKey("CoinInventorySavePlayerX00");
        }
    }
}
