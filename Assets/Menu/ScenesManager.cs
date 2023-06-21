using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string startMenuScene;
    public string GameScene;

    public static ScenesManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(startMenuScene);
    }
    public void LoadVersus()
    {
        SceneManager.LoadScene(GameScene);
    }

}
