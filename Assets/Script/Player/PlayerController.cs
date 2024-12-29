using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Stats stats;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    public CinemachineVirtualCamera vcam;
    
    protected Vector3 movement;
    private bool flip = true;

    private void Start()
    {
        vcam = GameObject.FindGameObjectWithTag("Vcam").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = this.transform;
    }

    private void Update()
    {
        Movement();
        if (stats.hp <= 0)
        {
            Death();
        }
    }

    private void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(movement.x, movement.y) * stats.speed;
        
        anim.SetFloat("Speed", Mathf.Abs(movement.x));
        
        if(movement.x != 0) rb.AddForce(new Vector2(movement.x * stats.speed, 0));
        if(movement.x > 0 && !flip) Flip();
        if(movement.x < 0 && flip) Flip();
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        flip = !flip;
    }

    private void Death()
    {
        anim.SetBool("IsDead", true);
        Destroy(this.gameObject, 0.5f);
    }
}
