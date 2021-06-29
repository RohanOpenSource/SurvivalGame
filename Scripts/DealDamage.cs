using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private int damageAmount;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && enemy.isAttacking){
            other.GetComponent<Life>().TakeDamage(damageAmount);
        }
    }
}
