using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StoryMode()
    {
        PlayerPrefs.SetString("PlayerID", "PlayerX00");
        Debug.Log(PlayerPrefs.GetString("PlayerID"));
        return;
    }

    public void EndlessMode()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
