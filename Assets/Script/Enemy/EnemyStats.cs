using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHp, hp, atk;
    public float AtkRange, sight, speed, atkSpeed, CD;
    
    private void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {

    }

}
