using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsBuff : MonoBehaviour
{
    [SerializeField] private bool hp, mp, spd, shield, critRate, shieldRegenRate, shieldRegenDelay;
    [SerializeField] private Stats stats;
    [SerializeField] private float buffValue;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();  
        if (hp) descriptionText.text += "MaxHP + " + (buffValue * 100) + "%\n";

        if (mp) descriptionText.text += "MaxMP + " + (buffValue * 100) + "%\n";

        if (spd) descriptionText.text += "Speed + " + (buffValue * 100) + "%\n";
        
        if(shield) descriptionText.text += "MaxShield + " + (buffValue * 100) + "%\n";
        
        if(critRate) descriptionText.text += "Crit Rate + " + (buffValue * 100) + "%\n";
        
        if(shieldRegenDelay) descriptionText.text += "Shield Regen - " + buffValue + "s\n";
        
        if(shieldRegenRate) descriptionText.text += "Shield Regen + " + buffValue + "%\n";
    }

    public void ChooseBuff()
    {
        if (hp)
        {
            stats.maxHp += Mathf.RoundToInt(stats.maxHp * buffValue);
            stats.hp += Mathf.RoundToInt(stats.hp * buffValue);
        }

        if (mp)
        {
            stats.maxMp += Mathf.RoundToInt(stats.maxMp * buffValue);
            stats.mp += Mathf.RoundToInt(stats.mp * buffValue);
        }

        if (spd)
        {
            stats.speed += stats.speed * buffValue;
        }
        
        if(shield)
        {
            stats.maxShield += Mathf.RoundToInt(stats.maxShield * buffValue);
            stats.shield += Mathf.RoundToInt(stats.shield * buffValue);
        }
        
        if(critRate)
        {
            stats.critRate += buffValue;
        }
    }

    public void TurnOffPanel()
    {
        Time.timeScale = 1.0f;
        this.transform.parent.gameObject.SetActive(false);
    }
}
