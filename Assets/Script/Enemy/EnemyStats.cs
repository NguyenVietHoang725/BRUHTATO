using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHp, hp, atk, level;
    public float AtkRange, sight, speed;
    
    private void Start()
    {
        maxHp *= level;
        atk *= level;
        hp = maxHp;
    }

    private void Update()
    {

    }

}
