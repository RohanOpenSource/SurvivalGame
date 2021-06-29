using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    [SerializeField] private Slider slider;
    [SerializeField] private Canvas canvas;
    private float maxHealth;
    private void Start() {
        maxHealth = health;
        slider.value = health/maxHealth;
        canvas.worldCamera = Camera.main;
        slider.gameObject.SetActive(false);
    }
    public void TakeDamage(float damage){
        health -= damage;
        slider.gameObject.SetActive(true);
        slider.value = health/maxHealth;
        if(health<=0){
            Destroy(gameObject);   
        }
    }
}
