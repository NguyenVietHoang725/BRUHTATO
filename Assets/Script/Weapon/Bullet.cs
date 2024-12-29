using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    public float speed;
    [SerializeField] private Stats playerStats;
    
    private float countdown;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        countdown = 1 / playerStats.atkSpeed;
    }

    private void Update()
    {
        if (countdown > 0)
            countdown -= Time.deltaTime;
        else
            countdown = 0;
        if (countdown == 0 && playerStats.mp >= this.GetComponent<WeaponStats>().mpConsume)
        {
            if(this.GetComponent<WeaponStats>().rechargable && Input.GetButtonUp("Fire1")) BulletSpawn();
        }
    }

    void BulletSpawn()
    {
        var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = spawnPoint.right * speed;
        playerStats.mp -= this.GetComponent<WeaponStats>().mpConsume;
        countdown = 1 / playerStats.atkSpeed;
    }
}
