using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 movement;
    private bool flip = true;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = this.GetComponent<EnemyStats>();
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.hp <= 0)
            Death();
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (Vector3.Distance(target.position, transform.position) <= stats.sight &&
            Vector3.Distance(target.position, transform.position) >= stats.AtkRange)
        {
            movement.x = target.position.x - transform.position.x;
            movement.y = target.position.y - transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, target.position, stats.speed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        anim.SetFloat("Speed", rb.velocity.magnitude);
        if(movement.x > 0 && !flip) Flip();
        if(movement.x < 0 && flip) Flip();
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        
        flip = !flip;
    }
    
    private void Death()
    {
        anim.SetBool("IsDead", true);
        Destroy(this.gameObject, 0.5f);
    }
}