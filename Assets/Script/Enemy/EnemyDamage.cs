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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damage = stats.atk; 
            
            if(other.gameObject.GetComponent<Stats>().shield != 0)
                other.gameObject.GetComponent<Stats>().shield -= damage;
            else
                other.gameObject.GetComponent<Stats>().hp -= damage;
        }
    }
}