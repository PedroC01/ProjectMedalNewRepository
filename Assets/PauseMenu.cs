using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu1;
    [SerializeField]
    public GameObject pauseMenu2;
 

  

    public void pausePanelActive(int index, PlayerInputHandler pih)
    {
        if(index== 0) pauseMenu1.SetActive(true);
        if(index==1) pauseMenu2.SetActive(true);   
   
    }
    public void pausePanelInactive(int index,PlayerInputHandler pih)
    {
       
        if (index == 0) {
            
            pauseMenu1.SetActive(false);
        }
        if (index == 1) { 

            pauseMenu2.SetActive(false);
        }
            
    }
}
