using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public float speed;
    [SerializeField] private WeaponStats stats;
    [SerializeField] private Stats playerStats;
    
    private float countdown;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        countdown = 1 / stats.atkSpeed;
    }

    private void Update()
    {
        if (countdown > 0)
            countdown -= Time.deltaTime;
        else
            countdown = 0;
        if (stats.rechargable && Input.GetButtonUp("Fire1"))
        {
            BulletSpawn();
        }
    }

    void BulletSpawn()
    {
        if(stats.mpConsume <= playerStats.mp)
        {
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = spawnPoint.right * speed;
            playerStats.mp -= stats.mpConsume;
            countdown = 1 / stats.atkSpeed;
        }
    }
}
