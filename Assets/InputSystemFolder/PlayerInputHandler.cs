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
    
    [SerializeField] 
    private float doubleTapTimeThreshold = 1;
    private float lastTapTime;
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
    private bool IsDoubleTap()
    {
        float timeSinceLastTap = Time.time - lastTapTime;

        if (timeSinceLastTap <= doubleTapTimeThreshold)
        {
            lastTapTime = 0f;
            return true;
        }
        else
        {
            lastTapTime = Time.time;
            StartCoroutine(ResetLastTapTime());
            return false;
        }
    }


    private IEnumerator ResetLastTapTime()
    {
        yield return new WaitForSeconds(doubleTapTimeThreshold);
        lastTapTime = 0f;
    }




    public void OnMove(CallbackContext context)
    {
        if(pMovement!=null)
        pMovement.OnMove(context.ReadValue<Vector2>());
    }
    public void OnJump(CallbackContext context)//por algum motivo o call back context d� erro aqui
    {

        if (context.performed)
        {
            if (IsDoubleTap()==true)
            {

              
                pMovement.OnDash();
            }
            else
            {

           
                pMovement.OnJump();
            }
        }

        

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
