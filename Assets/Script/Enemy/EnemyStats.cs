using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHp, hp, atk;
    public float speed, atkSpeed, CD;
    
    private void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {
        Death();
    }

    void Death()
    {
        if(hp <= 0)
            Destroy(this.gameObject);
    }
}
