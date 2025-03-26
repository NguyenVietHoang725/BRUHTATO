using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int atk, mpConsume;
    public float atkSpeed, critRate;
    public bool rechargable, multishoot;

    [SerializeField] private Stats player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Stats>();
        player.critRate += critRate;
    }

    public string StatsToString()
    {
        return "\nATK: " + atk + "\nMP Consume: " + mpConsume
               + "\nAttack Speed: " + Math.Round(atkSpeed, 2)
               + "\nCrit Rate: " + Math.Round(critRate, 2);
    }
}
