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
    private BattleManager bm;
    public static bool play;
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
        bm= FindObjectOfType<BattleManager>();
     
   
    }
  
    void Update()
    {
        CheckCombo();
    }




    public void OnLeftTriggerPressed(CallbackContext context)
    {
        if (play == true)
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

    }

   

    public void OnRightTriggerPressed(CallbackContext context)
    {
        if (play == true)
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
    }

    

    private void CheckCombo()
    {
        if (play == true)
        {
            if (leftTriggerPressed && rightTriggerPressed)
            {
                PerformComboAction();
            }
        }
    }

    private void PerformComboAction()
    {
        if (play == true)
        {
            if (ph.canGoBerserk == true)
            {
                MpC.UseMedaForce();
                ph.canGoBerserk = false;
            }

        }
    }


    public void OnNorth(CallbackContext context)
    {
        if (play == true)
        {
            OI?.OnNorth();
        }
    }


    public void OnMove(CallbackContext context)
    {
        if (play == true)
        {
            pMovement?.OnMove(context.ReadValue<Vector2>());
        }   
    }
    public void OnJump(CallbackContext context)
    {

        if (play == true)
        {
            pMovement?.OnJump();
        }

    }
 
  /*  public void OnDash(CallbackContext context)
    {
      

          pMovement?.OnDash();

    }*/
    public void OnEast(CallbackContext context)
    {
        if (play == true)
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
    }
    public  void OnWest(CallbackContext context)
    {
        if (play == true)
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
        
    }

  /*  public void OnWestRelease(CallbackContext context)
    {

        pMovement.OnWestRelease();
     

     
    }*/
    public void L1(CallbackContext context)
    {
        if (play == true)
        {
            hold = context.action.IsPressed();
            pMovement?.R1(hold);
        }
      
    }
    public void R1(CallbackContext context)
    {
        if (play == true)
        {
            pMovement?.OnDash();
        }
    }

    public void LockOnOrNot(CallbackContext context)
    {
        //  LO?.LeftShoulderL1(); used to choose if i wanted to make lock on or not
    }
    public void DPaddUp(CallbackContext context)
    {
        if (play == true)
        {
            LO?.DPadUp();
        }
    }
    public void DPaddLeft(CallbackContext context)
    {
        if (play == true)
        {
            LO?.DPadLeft();
        }
    }
    public void DPaddRight(CallbackContext context)
    {
        if (play == true)
        {
            LO?.DPadRight();
        }
    }
    public void DPaddDown(CallbackContext context)
    {
        if (play == true)
        {
            LO?.DPadDown();
        }
        
      
    }
   
}
