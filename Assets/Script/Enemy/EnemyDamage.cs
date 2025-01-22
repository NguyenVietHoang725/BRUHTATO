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
            if(collider.gameObject.GetComponent<Stats>().shield != 0)
                collider.gameObject.GetComponent<Stats>().shield -= damage;
            else
                collider.gameObject.GetComponent<Stats>().hp -= damage;
        }
    }
}