using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHp, hp, maxMp, mp, maxShield, shield;
    public float speed, critRate, CD;

    private void Start()
    {
        hp = maxHp;
        mp = maxMp;
        maxShield = maxHp;
        shield = maxShield;
    }

    private void Update()
    {
        if(hp > maxHp) hp = maxHp;
        if(mp > maxMp) mp = maxMp;
        if(shield > maxShield) shield = maxShield;
        if(mp < 0) mp = 0;
        if(shield < 0) shield = 0;
    }
}
