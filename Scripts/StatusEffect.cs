using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
   
    public PlayerMovement playerMovement;
    [SerializeField] public Effect[] effects=new Effect[5];
    // Start is called before the first frame update
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoEffect(Effect effect)
    {
        float oldSpeed = playerMovement.speed;
        float oldJumpForce = playerMovement.jumpForce;

        playerMovement.speed = playerMovement.speed*effect.speedChange;
        playerMovement.jumpForce = playerMovement.jumpForce * effect.jumpChange;
        yield return new WaitForSeconds(effect.duration);

        playerMovement.speed = oldSpeed;
        playerMovement.jumpForce = oldJumpForce;
        
    }

    void Effect(string effectName)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].name == effectName)
            {
                DoEffect(effects[i]);
            }
        }
    }


    
}

public struct Effect
{
    public int duration;
    public float speedChange;
    public float jumpChange;
    public string name;
}
