using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ScenesManagerController : MonoBehaviour
{
    public static ScenesManagerController instance;
    private void Awake()
    {
        instance = this;
    }


    public string startMenuScene;
    public string GameScene;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
    public void LoadVersus()
    {
        SceneManager.LoadScene(GameScene);
    }

}
