using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float sprintBoost;
    [SerializeField] private Life player;
    bool isGrounded = false;
    private float staminaReductionTimer;
    private float gravitationalVelocity;
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, ground);
        staminaReductionTimer += Time.deltaTime;
        Move();
        ApplyGravity();
    }
    private void Move(){
        float mX = Input.GetAxisRaw("Horizontal");
        float mY = Input.GetAxisRaw("Vertical");
        Vector3 direction = (transform.forward * mY + transform.right * mX).normalized;
        
        if(Input.GetKey(KeyCode.LeftControl) && mY != -1 && isGrounded && player.GetStamina() > 0){
            direction += transform.forward * mY * sprintBoost;
            if(staminaReductionTimer >= 1){
                staminaReductionTimer = 0;
                player.TakeStamina(5);
            }
        }
        controller.Move(direction * speed * Time.deltaTime);
    }
    private void ApplyGravity(){
        if(!isGrounded)gravitationalVelocity += gravityMultiplier * Time.deltaTime * Time.deltaTime;
        else if(Input.GetKey(KeyCode.Space)) gravitationalVelocity = jumpForce;
        else gravitationalVelocity = 0;
        controller.Move(new Vector3(0,gravitationalVelocity,0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            speed = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Water")
        {
            speed = 3;
        }
    }

}
