using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int maxHp, maxMp, atk, mpConsume;
    public float speed, atkSpeed, critRate, critDmg, CD;
    public bool rechargable, multishoot;

    [SerializeField] private Stats player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Stats>();;
        player.maxHp += maxHp;
        player.maxMp += maxMp;
        player.atk += atk;
        player.speed *= 1 + speed;
        player.atkSpeed *= 1 + atkSpeed;
        player.critRate += critRate;
        player.critDmg += critDmg;
    }
}
