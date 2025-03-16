using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

public class WeaponController : MonoBehaviour
{
    private float countdown;
    private bool flip = true;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private WeaponStats stats;
    public Animator anim;
    
    private Vector3 movement;

    public float angle;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        stats = this.GetComponent<WeaponStats>();
    }

    private void Update()
    {
        if(!player)
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        if(countdown > 0) countdown -= Time.deltaTime;
        else countdown = 0;
        
        Rotation();
        
        if(player.stats.hp <= 0)
            Despawn();
        
        AttackAnimation();
    }

    private void Rotation()
    {
        transform.position = player.transform.position;
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        
        transform.right = direction;
        
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if((angle < -90 || angle > 90) && flip) Flip();
        if( angle > -90 && angle < 90 && !flip) Flip();
    }

    private void Flip()
    {
        Vector3 playerScale = player.transform.localScale;
        Vector3 currentScale = transform.localScale;
        
        playerScale.x *= -1;
        currentScale.y *= -1;
        
        transform.localScale = currentScale;
        player.transform.localScale = playerScale;
        
        flip = !flip;
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
        countdown = 1 / stats.atkSpeed;
    }

    private void Despawn()
    { 
        Destroy(this.gameObject);
    }
}
