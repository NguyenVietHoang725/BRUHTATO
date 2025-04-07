using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHp, hp, maxMp, mp, maxShield, shield, shieldRegenRate;
    public float speed, critRate, shieldRegenDelay;
    
    public Coroutine shieldRegenCoroutine;

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
    
    public void TakeDamage(int damage)
    {
        if(shield > 0)
            shield -= damage;
        else
            hp -= damage;

        if (shieldRegenCoroutine != null)
        {
            StopCoroutine(shieldRegenCoroutine);
        }

        shieldRegenCoroutine = StartCoroutine(RegenShieldAfterDelay());
    }

    private IEnumerator RegenShieldAfterDelay()
    {
        yield return new WaitForSeconds(shieldRegenDelay);

        while (shield < maxShield)
        {
            shield += 1;
            yield return new WaitForSeconds(shieldRegenDelay);
        }
    }
}
