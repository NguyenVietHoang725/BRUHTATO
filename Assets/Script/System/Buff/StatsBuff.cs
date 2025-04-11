using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StatsBuff : MonoBehaviour
{
    [SerializeField] private bool hp, mp, spd, shield, critRate, shieldRegenRate, shieldRegenDelay;
    [SerializeField] private Stats stats;
    [SerializeField] private float buffValue;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
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
        StartCoroutine(TurnOffPanelCoroutine());
    }

    private IEnumerator TurnOffPanelCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        enemySpawner.canSpawn = true;
        enemySpawner.SetAmount();
        stats.gameObject.GetComponent<PlayerController>().canMove = true;
        stats.gameObject.GetComponent<PlayerController>().canAttack = true;
        this.transform.parent.gameObject.SetActive(false);
    }
}
