using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    private EnemyStats stats;

    void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        damage = stats.atk;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Stats>().TakeDamage(damage);
        }
    }
}