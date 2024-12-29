using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall") || collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
