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
        if(!PlayerPrefs.HasKey("MainInventorySave" + PlayerPrefs.GetString("PlayerID")))
            syncButton.interactable = false;
    }
}
