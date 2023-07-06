using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovements pMovement;
    PlayerInputManager playerInputManager;
    public static int playerNo;
    private Shooter eastButton;
    private RocketLaucher northButton;
    private double lastClickTime = 0;
    private const double doubleClickTimeThreshold = 0.5f;
    private bool hold;
    private bool leftTriggerPressed;
    private bool rightTriggerPressed;
    [Header("Scripts")]
    public GameObject thisPlayer;
    public PlayerMedapartsController MpC;
    public PlayerHealth ph;
    public LockOn LO;
    private OverrideInput OI;

    void Start()
    {
        playerInput=GetComponent<PlayerInput>();
        var pMovements=FindObjectsOfType<PlayerMovements>();
        var index=playerInput.playerIndex;
        var eastButtons = FindObjectsOfType<Shooter>();
        var northButtons = FindObjectsOfType<RocketLaucher>();
        pMovement = pMovements.FirstOrDefault(m => m.GetPlayerIndex() == index);
        thisPlayer = pMovement.gameObject;
        MpC = thisPlayer.GetComponent<PlayerMedapartsController>();
        ph = thisPlayer.GetComponent<PlayerHealth>();
        LO = thisPlayer.GetComponent<LockOn>();
        OI = thisPlayer.GetComponent<OverrideInput>();
    }
  
    void Update()
    {
        CheckCombo();
    }




    public void OnLeftTriggerPressed(CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            leftTriggerPressed = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            leftTriggerPressed = false;
        }


    }

   

    public void OnRightTriggerPressed(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            rightTriggerPressed = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            rightTriggerPressed = false;
        }
    }

    

    private void CheckCombo()
    {
        if (leftTriggerPressed && rightTriggerPressed)
        {
            PerformComboAction();
        }
    }

    private void PerformComboAction()
    {
        if(ph.canGoBerserk == true)
        {
            MpC.UseMedaForce();
        }
 

    }


    public void OnNorth(CallbackContext context)
    {
        OI?.OnNorth();
    }


    public void OnMove(CallbackContext context)
    {
        pMovement?.OnMove(context.ReadValue<Vector2>());
    }
    public void OnSouth(CallbackContext context)
    {

        if (context.performed)
        {
            double currentTime = context.time;

            // Check if the time difference between the current click and the last click is within the threshold
            if (currentTime - lastClickTime <= doubleClickTimeThreshold)
            {
                PerformDoubleSouthClickAction();

            }
            else
            {
                PerformSingleSouthClickAction();


            }

            lastClickTime = currentTime;
        }

    }
    private void PerformDoubleSouthClickAction()
    {
        pMovement?.OnDash();
        lastClickTime = 0;
    }

    private void PerformSingleSouthClickAction()
    {
        // Code to execute for a single-click
        pMovement?.OnJump();
        // Reset lastClickTime after performing the action
        lastClickTime = 0;
    }
    public void OnDash(CallbackContext context)
    {
      

          pMovement?.OnDash();

    }
    public void OnEast(CallbackContext context)
    {
        if (context.started)
        {
           OI?.OnEast();
        }
        if (context.performed)
        {
            OI?.OnEast();
        }
        if (context.canceled)
        {
            OI?.OnEastRelease();
        }

    }
    public  void OnWest(CallbackContext context)
    {
        if (context.started)
        {
            OI?.OnWest();
        }
        if (context.performed)
        {
            OI?.OnWest();
        }
        if (context.canceled)
        {
            OI?.OnWestRelease();
        }
       
        
    }

  /*  public void OnWestRelease(CallbackContext context)
    {

        pMovement.OnWestRelease();
     

     
    }*/
    public void L1(CallbackContext context)
    {
        LO?.LeftShoulderL1();
    }
    public void R1(CallbackContext context)
    {
        hold = context.action.IsPressed();
        pMovement?.R1(hold);

    }
    public void DPaddUp(CallbackContext context)
    {
        LO?.DPadUp();
    }
    public void DPaddLeft(CallbackContext context)
    {
        LO?.DPadLeft();
    }
    public void DPaddRight(CallbackContext context)
    {
       LO?.DPadRight();
    }
    public void DPaddDown(CallbackContext context)
    {
       LO?.DPadDown();
    }
   
}
