using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private Transform target;
    private Rigidbody2D rb;
    private Vector3 movement;
    private bool flip = true;
    private float counter;
    [SerializeField] private GameObject ManaOrb;
    
    public Animator anim;
    public float cooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = this.GetComponent<EnemyStats>();
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        counter = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > 0) counter -= Time.deltaTime;
        else counter = 0;

        if (stats.hp <= 0)
            anim.SetBool("IsDead", true);
        
        Movement();
        
        if(counter == 0)
            Attack();
    }

    private void Movement()
    {
        if (Vector3.Distance(target.position, transform.position) <= stats.sight &&
            Vector3.Distance(target.position, transform.position) >= stats.AtkRange)
        {
            movement.x = target.position.x - transform.position.x;
            movement.y = target.position.y - transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, target.position, stats.speed * Time.deltaTime);
            anim.SetFloat("Speed", transform.position.magnitude);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        if(movement.x > 0 && !flip) Flip();
        if(movement.x < 0 && flip) Flip();
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, target.position) <= stats.AtkRange)
        {
            movement = Vector3.zero;
            anim.SetBool("IsAttack", true);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        
        flip = !flip;
    }

    private void AttackOff()
    {
        counter = cooldown;
        anim.SetBool("IsAttack", false);
    }
    
    private void Death()
    {
        var mpOrb = Instantiate(ManaOrb, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject, 0.5f);
    }
}