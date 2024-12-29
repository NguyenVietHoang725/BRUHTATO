using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class WeaponController : MonoBehaviour
{
    public float rotationSpeed;
    private float angle, countdown;
    
    [SerializeField] private PlayerController player;
    public Animator anim;
    
    private Vector3 movement;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(countdown > 0) countdown -= Time.deltaTime;
        else countdown = 0;
        
        Movement();
        
        if(player.stats.hp <= 0)
            Despawn();
        
        AttackAnimation();
    }

    private void Movement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(movement.x, movement.y);
        move.Normalize();

        transform.position = player.transform.position;
        if(movement.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if(movement.x < 0) transform.localScale = new Vector3(1, -1, 1);

        if (move != Vector2.zero)
        {
            angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void AttackAnimation()
    {
        if (Input.GetButton("Fire1"))
        {
            if(countdown == 0) anim.SetBool("Attack", true);
        }
        if(this.GetComponent<WeaponStats>().multishoot && !Input.GetButton("Fire1"))
            anim.SetBool("Attack", false);
    }

    private void AttackAnimOff()
    {
        anim.SetBool("Attack", false);
        countdown = 1 / player.GetComponent<Stats>().atkSpeed;
    }

    private void Despawn()
    { 
        Destroy(this.gameObject);
    }
}
