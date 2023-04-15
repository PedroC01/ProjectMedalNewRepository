using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.Mathematics;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float size = 10;
    [SerializeField]
    private Vector3 playerVelocity;
    private Vector3 move;
    public Camera camera1;
    [SerializeField]
    private float playerSpeed = 2.0f;


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
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 up;
    public Rigidbody rb;
    private Vector3 horizontalInput = Vector2.zero;
    public bool count;
    public int playerIndex;
    public float DamageReduction;
    [SerializeField]
    public Animator UiNorthAttack;
    public Shooter shooter;
    public LockOn LO;
    public RocketLaucher RL;
    public GameObject Enemy;
    public Animator m_Animator1;
    public bool block;
    public float dist;
    public float distClose;
    public bool closeRange = false;

    //Unity Events------------------------------
    [SerializeField]
    public UnityEvent On;
    [SerializeField]
    public UnityEvent Off;

 

    private void Awake()
    {
        setupJump();
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.camera1 = FindObjectOfType<Camera>();
        this.shooter = GetComponentInChildren<Shooter>();
        this.LO = GetComponent<LockOn>();
        this.RL = GetComponentInChildren<RocketLaucher>();
       
        m_Animator1 = GetComponentInChildren<Animator>();
        jumped = false;
        if (this.gameObject.GetComponent<Player1>() == true)
        {
           
            this.Enemy = FindObjectOfType<Player2>().gameObject;
          
        }



      
        if (this.gameObject.GetComponent<Player2>() == true)
        {
           
            this.Enemy = FindObjectOfType<Player1>().gameObject;
            
        }
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
        horizontalInput = direction;
    }
    public void OnJump()
    {
        if (isGrounded == true && jumped == false)
        {
            this.jumped = true;
            UiNorthAttack.SetTrigger("Jump");
            countTime = maxJumpTime;
        }


        // StartCoroutine(Jump(this.jumped));

    }
    public void OnEast()
    {
        shooter.East();
    }
    public void OnWest()
    {
        shooter.West();
    }
    public void northButton()
    {
        RL.North();
    }
    public void L1()
    {
        LO.LeftShoulderL1();
    }
    public void L2()
    {

    }
    public void R1(bool Hold)
    {
        Debug.Log("Reduce Damage");
        block = Hold;

        m_Animator1.SetBool("Blocking", block);

    }
    public void DPaddUp()
    {
        LO.DPadUp();
    }
    public void DPaddLeft()
    {
        LO.DPadLeft();
    }
    public void DPaddRight()
    {
        LO.DPadRight();
    }
    public void DPaddDown()
    {
        LO.DPadDown();
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
        if (block)
        {

        }

        if (horizontalInput.x != 0 || horizontalInput.y != 0)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
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
            }

        }


        var dir = new Vector3(Mathf.Cos(Time.time * playerSpeed) * size, Mathf.Sin(Time.time * playerSpeed) * size);

        /* if (isGrounded == false)
         {
             handleGravity();
         }*/

        HandleJump();


        dist = Vector3.Distance(Enemy.transform.position, this.transform.position);
        if (dist <= distClose)
        {
            closeRange = true;
            Off.Invoke();
        }
        else
        {
            closeRange = false;
            On.Invoke();
        }
        if (count == true)
        {
            countTime -= Time.deltaTime;
            if (countTime <= maxJumpTime / 2)
            {
                jumped = false;
                countTime = maxJumpTime;
                count = false;
            }
        }
       
    }


    private void FixedUpdate()
    {
        if (horizontalInput.x != 0 || horizontalInput.y != 0)
        {
            float oldy = this.rb.velocity.y;
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








    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") && isGrounded == false)
        {
            isGrounded = true;
            jumped = false;

        }
       
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") && isGrounded == false)
        {
            isGrounded = true;
         

        }

    }




}
