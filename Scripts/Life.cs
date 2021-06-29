using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private GameObject deadUI;
    [SerializeField] private Text timeSurvived;
    [SerializeField] private int health;
    [SerializeField] private int stamina;
    [SerializeField] private float healthRecoveryRate;
    [SerializeField] private float staminaRecoveryRate;
    private int maxHealth;
    private int maxStamina;
    private float healthTimer;
    private float staminaTimer;
    private float timerSurvived;
    private void Start() {
        maxHealth = health;
        maxStamina = stamina;
    }
    private void Update() {
        timerSurvived += Time.deltaTime;
        healthTimer += Time.deltaTime;
        staminaTimer += Time.deltaTime;
        if(health < maxHealth && healthTimer >= healthRecoveryRate){
            healthTimer = 0;
            health++;
        }
        if(stamina < maxStamina && staminaTimer >= staminaRecoveryRate && !Input.GetKey(KeyCode.LeftControl)){
            staminaTimer = 0;
            stamina++;
        }
        staminaBar.fillAmount = (float) stamina / (float) maxStamina;
        healthBar.fillAmount = (float) health / (float) maxHealth;
    }
    public void TakeDamage(int amount){
        health -= amount;
        if(health <= 0) Die();
    }
    public void TakeStamina(int amount){
        stamina -= amount;
    }
    public int GetStamina(){
        return stamina;
    }

    private void Die(){
        deadUI.SetActive(true);
        Time.timeScale = 0;
        timeSurvived.text = "Survived: " + (int) timerSurvived + " Seconds";
    }
    public void Respawn(){
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Application.Quit();
    }
}
