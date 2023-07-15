using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagerController : MonoBehaviour
{
    public static ScenesManagerController instance;
    private void Awake()
    {
        instance = this;
        Time.timeScale = 0;
    }
    public PauseMenu pm;

    public string startMenuScene;
    public string GameScene;
    public int BattleScene;
    public BattleManager bm;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
    public void LoadVersus()
    { 
        //Chamada input enable = false *****************************************************************************************
        SceneManager.LoadScene(1);
       
    }
    
    public void Pause(int index, PlayerInputHandler pih)
    {
        Scene scene = SceneManager.GetActiveScene();
        pm=FindObjectOfType<PauseMenu>();
        bm = FindObjectOfType<BattleManager>();
        if (this.BattleScene == scene.buildIndex)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                bm.IsNotPlaying();
                
                pm.pausePanelActive(index,pih);
                return;
            }
            else if (Time.timeScale== 0)
            {
                bm.IsPlaying();
                Time.timeScale = 1;
                pm.pausePanelInactive(index,pih);
                return;
            }
        }
    }


  
}
