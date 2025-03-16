using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Stats player;
    [SerializeField] private WeaponStats wp;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Stats>();
        wp = this.GetComponent<WeaponStats>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            float damage = wp.atk;
            float critValue = Random.Range(0f, 1f);
            if (critValue <= player.critRate)
            {
                damage *= 1.5f;
            }
            collider.gameObject.GetComponent<EnemyStats>().hp -= (int)damage;
        }
    }
}
