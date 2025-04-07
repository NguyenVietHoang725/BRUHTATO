using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOrb : MonoBehaviour
{
    [SerializeField]
    private int mpIncrease;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Stats>().mp += mpIncrease;
            Destroy(this.gameObject);
        }
    }
}
