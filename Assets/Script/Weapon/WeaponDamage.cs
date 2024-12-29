using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Stats player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Stats>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            float damage = player.atk;
            float critValue = Random.Range(0f, 1f);
            if (critValue <= player.critRate)
            {
                damage *= player.critDmg;
            }
            collider.gameObject.GetComponent<EnemyStats>().hp -= (int)damage;
        }
    }
}
