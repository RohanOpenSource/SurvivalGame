using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private int movespeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float sightDistance;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform gfx;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float turnSpeed;
    [SerializeField] private int retreatHealth;
    private Transform player;
    private float timer;
    public bool isAttacking; // is accessed by the DealDamage class
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        timer+=Time.fixedDeltaTime;
        //the guy will float if i don't discount the y
        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist > stopDistance && dist <= sightDistance && !isAttacking)
        {
            //using rb.velocity makes sure that he doesn't accelerate which we dont want
            Vector3 relativePos = new Vector3(player.position.x, gfx.position.y, player.position.z) - gfx.position;
            Quaternion desiredRot = Quaternion.LookRotation(relativePos);
            gfx.rotation = Quaternion.Lerp(gfx.rotation, desiredRot, turnSpeed);
            rb.velocity=new Vector3(movespeed * direction.x, rb.velocity.y, movespeed * direction.z);
            anim.SetBool("Walking", true);
        }

        else if(dist <= stopDistance){
            Vector3 relativePos = new Vector3(player.position.x, gfx.position.y, player.position.z) - gfx.position;
            Quaternion desiredRot = Quaternion.LookRotation(relativePos);
            gfx.rotation = Quaternion.Lerp(gfx.rotation, desiredRot, turnSpeed);
            rb.velocity=Vector3.zero;
            anim.SetBool("Walking", false);
            if(timer>=timeBetweenAttacks){
                timer=0;
                Attack();
            }
        }

        else{
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetBool("Walking", false);
        }
        
    }
    private void Attack(){
        anim.SetTrigger("Attack");
        if(!isAttacking)StartCoroutine(Attacking());
    }
    private IEnumerator Attacking(){
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

}
