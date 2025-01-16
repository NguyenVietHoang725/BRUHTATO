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
        
    }
}
