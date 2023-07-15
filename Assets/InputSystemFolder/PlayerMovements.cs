using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.Mathematics;
using UnityEngine.VFX;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float size = 10;
    [SerializeField]
    private Vector3 playerVelocity;
    private Vector3 move;
    public Camera camera1;
    [SerializeField]
    public float playerSpeed = 2.0f;

    public bool isInvencible;
    public bool backStrafe = false;
    private Quaternion initialRotation;
    //jump---------------------------------------------------------
    public float groundDistance = 0.4f;
    float countTime;
    public float jumpExtraForce;
    [SerializeField]
    public float jumpForce = 1.0f;
    public float initialJumpVelocity;
    public float maxJumpHeight = 1.0f;
    public float maxJumpTime = 0.5f;
    public float gravityMultiplier;
    public float gravity = -9.8f;
    public float groundGravity = -2;
    public bool jumped;
    private bool canJump;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 up;
    public Rigidbody rb;
    public Vector3 horizontalInput = Vector2.zero;
    public bool count;
    public int playerIndex;
    public float DamageReduction;
    [SerializeField]
    public Shooter shooter;
    public LockOn LO;
    public RocketLaucher RL;
    public GameObject Enemy;
    public Animator m_Animator1;
    public bool block;
    public float dist;
    public float distClose;
    public bool closeRange = false;
    private bool collidingWithInivisi;
    //Unity Events------------------------------

    public bool IsMoving;
    public LayerMask obstacleLayerMask;

    [Header("Dash Related")]
    private bool canDash;
    public bool dash;
    private bool isDashing = false;
    public float dashDuration = 0.5f;
    public float currentDashTime;
    public float dashDistance = 5f;
    private Vector3 dashDirection;
    public float delayAfterDash;
    public float dashCoolDown;
    private float dashCoolDownReset;
    private PlayerMedapartsController pmc;

    public float recoveryTime = 2f;
    public GameObject dust;
    public int CheckIfCharacter;
    public bool canMove;
    public float timeToRotateToImpact;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;
    private Vector3 knockbackDirection;
    public bool KnockBack;
    private Vector3 onImpact;
    public bool jumpInactive;
    public bool isAlreadyKnockingBack;
    public bool ForceStop = false;
    private void Awake()
    {
        setupJump();
        this.pmc = GetComponent<PlayerMedapartsController>();
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.LO = GetComponent<LockOn>();
        jumpInactive = false;
        if (pmc.characterStatsSO.characterReferenceNumber == 1)
        {
            this.shooter = GetComponentInChildren<Shooter>();
            this.RL = GetComponentInChildren<RocketLaucher>();
        }

        // this.camera1 = FindObjectOfType<Camera>(); //para testar split screen-------------------------------


        this.m_Animator1 = GetComponentInChildren<Animator>();
        jumped = false;
        if (this.gameObject.GetComponent<Player1>() == true)
        {
           
            this.Enemy = FindObjectOfType<Player2>().gameObject;
          
        }
        if (this.gameObject.GetComponent<Player2>() == true)
        {

            this.Enemy = FindObjectOfType<Player1>().gameObject;

        }
        canMove = true;
        dashCoolDownReset = dashCoolDown;
        dashCoolDown = 0;
        GetComponent<PlayerHealth>().Enemy = this.Enemy;
        // Store the initial rotation of the character
        initialRotation = transform.rotation;
        collidingWithInivisi = false;

    }



    void setupJump()
    {
        float timeToApexJump = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApexJump, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApexJump;
    }



    public int GetPlayerIndex()
    {
        return playerIndex;
    }



    //input functions----------------------------------------------

    public void OnMove(Vector2 direction)
    {
        if (canMove)
        {
            horizontalInput = direction;
        }
    }
    public void OnJump()
    {
        if (jumpInactive == false) { 
        if (canJump)
        {


            if (isGrounded == true && jumped == false)
            {
                this.jumped = true;
                countTime = maxJumpTime;
                canJump = false;
            }

        }
            // StartCoroutine(Jump(this.jumped));
        }
        else
        {
            return;
        }
    }
    public void OnDash()
    {
        if (!isDashing)
        {
            if (isGrounded == true && !isDashing && canDash == true)
            {
                m_Animator1.SetBool("Dash", true);
                dashDirection = transform.forward;
                dash = true;
                StartCoroutine(Dash());
            }
        }
        else
        {
            return;
        }
           
        
      
    }
   
    
    public void L2()
    {

    }
    public void R1(bool Hold)
    {
        Debug.Log(block);
        block = Hold;
        pmc.SetBlocking(true);
        m_Animator1.SetBool("Blocking", block);

    }
   


    //------------------------------------------------------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
        // Gizmos.DrawSphere(this.transform.position,distClose);
    }

    


    void HandleJump()
    {

        if (jumped == true)
        {

            up = transform.up * (jumpExtraForce + initialJumpVelocity) * 0.5f;
            up.x = 0;
            up.z = 0;
            m_Animator1.SetBool("Jumping", true);
            this.rb.velocity = up;
            isGrounded = false;
            count = true;
           
        }


    }
    void handleGravity()
    {
        if (isGrounded == true)
        {

            this.rb.velocity += new Vector3(0, groundGravity, 0) * Time.deltaTime;
        }
        else
        {
            this.rb.velocity += new Vector3(0, gravity, 0) * Time.deltaTime;
        }
       

       


    }


    void Update()
    {
       
        if (block||ForceStop)
        {
            horizontalInput.x = 0;
            horizontalInput.y = 0;
        }
      
        if (horizontalInput.x != 0 || horizontalInput.y != 0)
        {
            IsMoving = true;
            Vector3 forward = camera1.transform.forward;
            Vector3 right = camera1.transform.right;
           // Vector3 forward = Camera.main.transform.forward;
          //  Vector3 right = Camera.main.transform.right;
            forward.y = 0;
            right.y = 0;
            forward = forward.normalized;
            right = right.normalized;
            move = horizontalInput.x * right + horizontalInput.y * forward;
            move *= playerSpeed;
     
                m_Animator1.SetFloat("Moving", playerSpeed);
            
           
            if (move != Vector3.zero)
            {
                transform.forward = move;

            }


        }
        else
        {
            m_Animator1.SetFloat("Moving", 0);//criar animator para o segundo jogador para o player 2 poder se mover
            if (horizontalInput.x == 0 && horizontalInput.y == 0 && isGrounded == true)
            {
                rb.velocity = new Vector3(0, groundGravity, 0);
                IsMoving = false;
            }

        }
        

        var dir = new Vector3(Mathf.Cos(Time.time * playerSpeed) * size, Mathf.Sin(Time.time * playerSpeed) * size);

        /* if (isGrounded == false)
         {
             handleGravity();
         }*/

        HandleJump();


        dist = Vector3.Distance(Enemy.transform.position, this.transform.position);
       
        if (count == true)
        {
            countTime -= Time.deltaTime;
            if (countTime <= maxJumpTime / 2)
            {
               
                jumped = false;
                countTime = maxJumpTime;
                m_Animator1.SetBool("Jumping", false);
                count = false;
            }
        }
        if (dashCoolDown > 0)
        {
            dashCoolDown-= Time.deltaTime;
        }
        else
        {
            canDash = true;
        }
        
    }


    private void FixedUpdate()
    {


        if (horizontalInput.x != 0 || horizontalInput.y != 0)
        {
            float oldy = rb.velocity.y;
           
            this.rb.velocity = this.rb.transform.forward * playerSpeed;
            this.rb.velocity = new Vector3(this.rb.velocity.x, oldy, this.rb.velocity.z);
            //this.rb.AddForce(transform.forward * playerSpeed, ForceMode.Force);

        }
       

        handleGravity();

    }




    /*  IEnumerator Jump(bool jumped_)
      {
          if (jumped_ == true)
          {

             // jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y));
              //this.rb.velocity = this.rb.transform.up * jumpForce;
             // rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
              isGrounded = false;
             yield return new WaitForSeconds(1);

          }
          yield return null;
      }*/

    private IEnumerator Dash()
    {
        isDashing = true;
        isInvencible = true;
        currentDashTime = 0f;
        canMove = false;
        horizontalInput.x = 0;
        horizontalInput.y = 0;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + dashDirection * dashDistance;

        // Check if the next position will collide with obstacles
        if (!CheckCollision(targetPosition))
        {
            while (currentDashTime < dashDuration)
            {
                float dashSpeed = dashDistance / dashDuration;
                float t = currentDashTime / dashDuration;
                Vector3 nextpos = Vector3.Lerp(initialPosition, targetPosition, t);

                // Move the player only in the dash direction
                nextpos += dashDirection * dashSpeed * Time.deltaTime;

                // Check if the next position will collide with obstacles
                if (!CheckCollision(nextpos))
                {
                    this.transform.position = nextpos;
                    this.currentDashTime += Time.deltaTime;
                }
                else
                {
                    break; // Stop dashing if there's an obstacle
                }

                yield return null;
            }
        }

        // Delay to exit the dash animation
        yield return new WaitForSeconds(delayAfterDash);

        dash = false;
        horizontalInput.x = 0;
        horizontalInput.y = 0;
        rb.velocity = new Vector3(0, 0, 0);
        m_Animator1.SetBool("Dash", false);
        isDashing = false;
        canDash = false;
        dashCoolDown = dashCoolDownReset;
        canMove = true;
        isInvencible = false;
    }
   







    private bool CheckCollision(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f);

        // verificar se o dash bate numa parede ou no outro jogador
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("InvisiWalls"))
            {
                return true; 
            }
        }

        return false; 
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") && isGrounded == false)
        { 

            isGrounded = true;

            if (isGrounded == true)
            {
                Vector3 dustpos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                Quaternion zeroRotation = Quaternion.identity;
                GameObject dust1 = Instantiate(dust, dustpos, zeroRotation);
            }
            jumped = false;
            canJump = true;
            m_Animator1.SetTrigger("Landed");


        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        onImpact = other.transform.position;
        if (!isInvencible)
        {
            if (other.GetComponent<Rocket>() != null && !KnockBack)
            {

                KnockBack = true;
                Debug.Log("Rocket");
                knockbackDirection = ( onImpact- transform.position).normalized;
                
                this.m_Animator1.SetBool("KnockBack", true);

                if(isAlreadyKnockingBack==false)StartCoroutine(KnockbackCoroutine());


            }
        }
       
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") && isGrounded == false)
        {
            isGrounded = true;
         

        }
        if (collision.collider.CompareTag("InvisiWalls"))
        {
            collidingWithInivisi = true;
        }
        else
        {
            collidingWithInivisi = false;
        }

    }

    public IEnumerator KnockbackCoroutine()
    {
        if (isAlreadyKnockingBack == false)
        {
            isAlreadyKnockingBack = true;

            bool cancelKnockback = false;
            float timer = 0f;
            KnockBack = true;

            // Calculate the rotation needed to face the hit position
            Quaternion targetRotation = Quaternion.LookRotation(knockbackDirection, Vector3.up);
            this.m_Animator1.SetBool("KnockBack", true);
            while (timer < knockbackDuration)
            {
                // Move the player in the opposite direction of knockbackDirection
                Vector3 newPosition = transform.position + (-knockbackDirection * knockbackForce * Time.deltaTime);

                // Check if the new position will collide with obstacles
                if (!CheckCollision(newPosition))
                {
                    transform.position = newPosition;
                    timer += Time.deltaTime;
                }
                else
                {
                    // Stop knockback if there's an obstacle
                    break;
                }

                // Rotate the player towards the hit position gradually
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * timeToRotateToImpact);
                if (KnockBack == true)
                {
                    Collider[] colliders = Physics.OverlapSphere(newPosition, 1f);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("InvisiWalls"))
                        {
                            cancelKnockback = true;
                            break;
                        }
                    }
                }

                if (cancelKnockback)
                {
                    break;
                }
                yield return null;
            }

            // Delay to exit the knockback animation
            yield return new WaitForSeconds(0.5f);
            isInvencible = true;
            if (cancelKnockback)
            {
                //  this.m_Animator1.SetBool("KnockBackHit0",true);
                //   yield break; 

            }
            KnockBack = false;
            this.m_Animator1.SetBool("KnockBack", false);
            //this.m_Animator1.SetBool("KnockBackHit0", false);
            isInvencible = false;
            isAlreadyKnockingBack = false;
        }
        else
        {
            yield return null;
        }
        
    }

}
