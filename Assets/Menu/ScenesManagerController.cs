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



    //Main Menu Sound Callbacks:
    #region Call Menu Sounds
    [Space(5)]
    [Header("Menu Sounds:")]
    //public List<AudioSource> listMenuSounds = new List<AudioSource>(); //Colocar os sons nesta list e chamar com metodo abaixo via codigo: MenuSoundsIndex(int musicIndex)
    public AudioSource menuMusicSound;
    //
    public AudioSource menuSubmitSound;
    public AudioSource menuCancelSound;
    public AudioSource menuNavigateSound;
    //
    //Versus
    public AudioSource menuVersusMusicSound;
    //
    public AudioSource menuReadySound;
    public AudioSource menuAllReadySound;
    //
    public void Play_MenuMusicSound()
    {
       // menuMusicSound.Play();
    }
    public void Play_MenuSubmitSound()
    {
      //  menuSubmitSound.Play();
    }
    public void Play_MenuCancelSound()
    {
       // menuCancelSound.Play();
    }
    public void Play_MenuNavigateSound()
    {
       // menuNavigateSound.Play();
    }
    //
    //Versus
    public void Play_MenuVersusMusicSound()
    {
//menuVersusMusicSound.Play();
    }

    public void Play_MenuReadySound()
    {
     //   menuReadySound.Play();
    }
    public void Play_MenuAllReadySound()
    {
     //   menuAllReadySound.Play();
    }



    //Antes com lista:
    //int 0 = menu int 1 = versus
    // [Header("Menu Sounds List:")]
    //  public List<AudioSource> listMenuSounds = new List<AudioSource>(); //Colocar os sons nesta list e chamar com metodo abaixo via codigo: MenuSoundsIndex(int musicIndex)

    /* void MenuSoundsIndex(int soundIndex)
     {
         listMenuSounds[soundIndex].Play();
     }*/
    #endregion
    //Main Menu Init/Reset?<- meto aqui ref mais easy de call all???!?!?!?


}
