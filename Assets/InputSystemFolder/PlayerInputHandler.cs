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
    private bool hold;
    private bool leftTriggerPressed;
    private bool rightTriggerPressed;

    void Start()
    {
        playerInput=GetComponent<PlayerInput>();
        var pMovements=FindObjectsOfType<PlayerMovements>();
        var index=playerInput.playerIndex;
        var eastButtons = FindObjectsOfType<Shooter>();
        var northButtons = FindObjectsOfType<RocketLaucher>();
        pMovement = pMovements.FirstOrDefault(m => m.GetPlayerIndex() == index);
      
    }
  
    void Update()
    {
   
    }

    private void OnEnable()
    {
        playerInput.actions["LeftTrigger"].performed += OnLeftTriggerPressed;
        playerInput.actions["RightTrigger"].performed += OnRightTriggerPressed;
        playerInput.actions["LeftTrigger"].canceled += OnLeftTriggerReleased;
        playerInput.actions["RightTrigger"].canceled += OnRightTriggerReleased;
    }

    private void OnDisable()
    {
        playerInput.actions["LeftTrigger"].performed -= OnLeftTriggerPressed;
        playerInput.actions["RightTrigger"].performed -= OnRightTriggerPressed;
        playerInput.actions["LeftTrigger"].canceled -= OnLeftTriggerReleased;
        playerInput.actions["RightTrigger"].canceled -= OnRightTriggerReleased;
    }

    private void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        leftTriggerPressed = true;
        CheckCombo();
    }

    private void OnLeftTriggerReleased(InputAction.CallbackContext context)
    {
        leftTriggerPressed = false;
    }

    private void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        rightTriggerPressed = true;
        CheckCombo();
    }

    private void OnRightTriggerReleased(InputAction.CallbackContext context)
    {
        rightTriggerPressed = false;
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
        // Perform your desired action when the button combo is detected
        Debug.Log("Button combo performed!");
    }


    public void OnMove(CallbackContext context)
    {
        if(pMovement!=null)
        pMovement.OnMove(context.ReadValue<Vector2>());
    }
    public void OnJump(CallbackContext context)//por algum motivo o call back context dá erro aqui
    {
   
                pMovement.OnJump();
       
    }
    public void OnDash(CallbackContext context)
    {
       
       
          
                pMovement.OnDash();
           
    }
    public void OnEast(CallbackContext context)
    {
        pMovement.OnEast();
    }
    public void OnWest(CallbackContext context)
    {
        if (context.started)
        {
            pMovement.OnWest();
        }
        if (context.performed)
        {
            pMovement.OnWest();
        }
        if (context.canceled)
        {
            pMovement.OnWestRelease();
        }
            
        
        
       
        ///criar release action----------------------------------------------------------------------------------------------------------------
    }
  /*  public void OnWestRelease(CallbackContext context)
    {

        pMovement.OnWestRelease();
     

     
    }*/
    public void L1(CallbackContext context)
    {
        pMovement.L1();
    }
    public void R1(CallbackContext context)
    {
        hold = context.action.IsPressed();
        pMovement.R1(hold);

    }
    public void DPaddUp(CallbackContext context)
    {
        pMovement.DPaddUp();
    }
    public void DPaddLeft(CallbackContext context)
    {
        pMovement.DPaddLeft();
    }
    public void DPaddRight(CallbackContext context)
    {
        pMovement.DPaddRight();
    }
    public void DPaddDown(CallbackContext context)
    {
        pMovement.DPaddDown();
    }
    public void OnNorth(CallbackContext context)
    {
        pMovement.northButton();
    }
}
