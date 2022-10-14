using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Characters : MonoBehaviour
{
   
    public bool isDead = false;
    public FixedJoystick _joystick;
    public Rigidbody rb;
    public Transform tf;
    public Animator animator;
    public float horizontalInput;
    public float verticalInput;
    public float moveSpeed;
    public int health;
    public float xRange;
    public float zRange;
    public static float jumpTimer = 0;
    public float healthTimer = 0;
    public float healthTimerDisplay = 10;
    public Vector3 movementDirection;
    public float rotationSpeed;
    public bool isJumping;
    public bool isGrounded; //here is grounded means white tile since player can walk on it

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        animator= GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Awake()
    {
    }
    private void Update()
    {
        
    }
    public void BasicMovement()
    {
        if (!isDead)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * moveSpeed);
            verticalInput = Input.GetAxis("Vertical");
            // transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * moveSpeed);
            movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            movementDirection.Normalize();
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        
            if (horizontalInput != 0 || verticalInput != 0)
            {
                healthTimer += Time.deltaTime;
            }
            if (movementDirection != Vector3.zero)
            {
                //IF Moving
                animator.SetBool("IsMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
    public void JoyStickMovement()
    {
        if (!isDead)
        {

            rb.velocity = new Vector3(_joystick.Horizontal * moveSpeed, rb.velocity.y, _joystick.Vertical * moveSpeed);
            movementDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                healthTimer += Time.deltaTime;
            }
            if (movementDirection != Vector3.zero)
            {
                //IF Moving
                animator.SetBool("IsMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
    
    public void InputLimit()
    {
        xRange = StageSpawner.stageLength;
        zRange = StageSpawner.stageLength;
        tf = GetComponent<Transform>();
        if (tf.position.x < -0.6f)
        {
            tf.position = new Vector3(-0.6f, tf.position.y, tf.position.z);
        }
        if (tf.position.z < -0.6f)
        {
            tf.position = new Vector3(tf.position.x, tf.position.y, -0.6f);
        }
        if (tf.position.x > xRange)
        {
            tf.position = new Vector3(xRange, tf.position.y, tf.position.z);
        }
        if (tf.position.z > zRange)
        {
            tf.position = new Vector3(tf.position.x, tf.position.y, zRange);
        }

    }
    public void HealthIncrease()
    {
            if (healthTimer > 10.0f)
            {
                health++;
                healthTimer = 0;
                FindObjectOfType<AudioManager>().Play("HPup");
            } 
    }
    public void BounceUp(GameObject gameObject)
    {
        rb.AddForce(Vector3.up * 6, ForceMode.Impulse);
    }
    public void CanJump()
    {
        if (!isDead)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer > 3 && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                
            }
        }
    }
    public void Jump()
    {
        //Play Jump Sound
        FindObjectOfType<AudioManager>().Play("Jump");
        rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        jumpTimer = 0;
    }
    public void Death()
    {
        {
            isDead = true;     // used to stop movement and play the game over screen
            //Play Death Anim
            animator.SetBool("IsDead", true);
            GameOver();
            //Destroy(gameObject);
        }
    }
    public void GameOver()
    {
        if (isDead == true)
        {
            StartCoroutine(WaitForSceneLoad());
        }
    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(4);
        
        SceneManager.LoadScene(2, LoadSceneMode.Single);
   
    }
    private void JumpAnimations()
    {
        if (transform.position.y > 0.4)
        {
            animator.SetBool("IsJumping", true);
        }
    }
 }

