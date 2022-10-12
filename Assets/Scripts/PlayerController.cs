using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : Characters
{
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
        moveSpeed = 3;
        health = 4;
        jumpTimer = 0;
        healthTimer = 0;
        rotationSpeed = 720;
    }

    // Update is called once per frame
    void Update()
    {
        healthIncreaseTimerDisplay.text = "Gain Life in :" + healthTimer + " seconds";
        healthDisplay.text = "Health : " + health.ToString();
        jumpTimer += Time.deltaTime;
        //base. keyword is used for inheritence
        BasicMovement();
        InputLimit();
        CanJump();
        HealthIncrease();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            if (health > 0.5f)
            {
                BounceUp(this.gameObject);
                health--;
            }
            else if (health < 1)
            {
                Death();
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
    
}
