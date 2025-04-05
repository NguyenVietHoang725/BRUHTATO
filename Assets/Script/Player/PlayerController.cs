using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Stats stats;

    private Rigidbody2D rb;
    private Animator anim;
    public CinemachineVirtualCamera vcam;
    
    private Vector3 movement;
    private InventoryInputManager inventoryInputManager;
    
    public bool canMove = true;
    public bool canAttack = true;

    private void Start()
    {
        inventoryInputManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryInputManager>();
        vcam = GameObject.FindGameObjectWithTag("Vcam").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if(inventoryInputManager != null)
        {
            if (!inventoryInputManager.InventoryIsOpen)
                canAttack = canMove = true;
            else
                canAttack = canMove = false;
        }
        
        if(canMove)
            Movement();
        if (stats.hp <= 0)
            anim.SetBool("IsDead", true);
    }

    private void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(movement.x, movement.y) * stats.speed;
        
        anim.SetFloat("Speed", rb.velocity.magnitude);
        
        if(movement.x != 0) rb.AddForce(new Vector2(movement.x * stats.speed, 0));
    }

    private void Death()
    {
        this.gameObject.SetActive(false);
    }
}
