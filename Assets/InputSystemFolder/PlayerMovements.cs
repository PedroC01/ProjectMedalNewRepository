using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float size = 10;
    [SerializeField]
    private Vector3 playerVelocity;
   
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    public float jumpForce = 1.0f;
    private Rigidbody rb;
    private Vector3 horizontalInput = Vector2.zero;

    public bool jumped = false;
    public int playerIndex;
    private Vector3 move;
    public Camera camera1;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Shooter shooter;
    public LockOn LO;
    public RocketLaucher RL;
    public GameObject Enemy;
    public bool block;
    public float dist;
    public float distClose;
    [SerializeField]
    public UnityEvent On;
    [SerializeField]
    public UnityEvent Off;

    public Animator m_Animator1;

    public bool closeRange=false;
    void Start()
    {
       this.rb=GetComponent<Rigidbody>();
       this.camera1=FindObjectOfType<Camera>();
        this.shooter=GetComponentInChildren<Shooter>();
        this.LO=GetComponent<LockOn>();
        this.RL = GetComponentInChildren<RocketLaucher>();
        this.Enemy = FindObjectOfType<Player2>().gameObject;
        m_Animator1 = GetComponentInChildren<Animator>();
    }



    public int GetPlayerIndex()
    {
        return playerIndex;
    }





    public void OnMove(Vector2 direction)
    {
        horizontalInput = direction;
    }
    public void OnJump()
    {
        this.jumped = true;
        StartCoroutine(Jump(this.jumped));
    }
    public void OnEast()
    {
        shooter.East();
    }
    public void OnWest()
    {
        shooter.West();
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
    public void northButton()
    {
        RL.North();
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
       // Gizmos.DrawSphere(this.transform.position,distClose);
    }



    void Update()
    {
        
        if (isGrounded==false)
        {
            move.y = 0;
            jumped = false;
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
            m_Animator1.SetFloat("Moving",0);//criar animator para o segundo jogador para o player 2 poder se mover
            this.rb.velocity = Vector3.zero;
        }

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

        var dir = new Vector3(Mathf.Cos(Time.time * playerSpeed) * size, Mathf.Sin(Time.time * playerSpeed) * size);
    }


    private void FixedUpdate()
    {
        
        if (horizontalInput.x != 0 || horizontalInput.y != 0)
        {

            this.rb.velocity = this.rb.transform.forward*playerSpeed;
            //this.rb.AddForce(transform.forward * playerSpeed, ForceMode.Force);
        }


    }




    IEnumerator Jump(bool jumped_)
    {
        if (jumped == true && isGrounded == true)
        {
            this.rb.velocity = this.rb.transform.up * jumpForce;
          //  rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            this.jumped = false;
            isGrounded = false;
        }
        yield return null;
    }








    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") && isGrounded == false)
        {
            isGrounded = true;

        }
    }


    

}
