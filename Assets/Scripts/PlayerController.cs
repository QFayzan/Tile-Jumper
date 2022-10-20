using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : Characters
{
    public static bool isRoundClear = false;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI healthIncreaseTimerDisplay;
    
    //maybe if i ever need to reset position
   // Vector3 startingPostion = new Vector3(-1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        _joystick = FindObjectOfType<FixedJoystick>();
        moveSpeed = 3;
        health = 4;
        jumpTimer = 0;
        healthTimer = 0;
        healthTimerDisplay = 10;
        rotationSpeed = 720;
        isRoundClear = false;
}

    // Update is called once per frame
    void Update()
    {
        if (isRoundClear)
        {
            OnRoundClear();
            isRoundClear = false;
        }
        healthIncreaseTimerDisplay.text = "keep moving for " + (healthTimerDisplay- healthTimer).ToString("F0") + " seconds to get more HP";
        healthDisplay.text = "Health : " + health.ToString();
        jumpTimer += Time.deltaTime;
        //base. keyword is used for inheritence
        
        InputLimit();
        CanJump();
        HealthIncrease();

        if (health < 1)
        {
            Death();
        }
    }
    private void FixedUpdate()
    {
        //physics based movement in fixed update
        //BasicMovement();
       JoyStickMovement();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            //Instant death
            //Death();
            //isDead = true;
          //GameOver();
            if (health > 0 && !isDead) 
             {
                  BounceUp(this.gameObject);
                  FindObjectOfType<AudioManager>().Play("Hurt");
                  health--;
             } 
           
              else if (health <=0 )
              {
                 Death();
                  //Play DeathSound
                 FindObjectOfType<AudioManager>().Play("Death");
              }
        }
        else if (other.gameObject.name==("White"))
            {
            animator.SetBool("IsGrounded", true);
            animator.SetBool("IsJumping", false);
            }
        else if (other.gameObject.name != "White")
        {
            animator.SetBool("IsGrounded", false);
        }
    }
    void OnRoundClear()
    {
        transform.position = new Vector3(StageSpawner.stageLength -0.5f, transform.position.y, StageSpawner.stageLength-0.5f);
    }
    
}
