using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using FMODUnity;
using FMOD.Studio;


public class SoundManagerStartMenu : MonoBehaviour
{

    public static SoundManagerStartMenu instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       // Play_MenuMusicSound();
    }
    //Main Menu Sound Callbacks:

 




    [Space(5)]
    [Header("Fmod:")]
    //StartMenu/Menu Effects
    public string navigateEffect;
    public string submitEffect;
    public string cancelEffect;

    //Versus Effects
    public string readyEffect;
    public string allReadyEffect;

    //Musics:
    //Menu Music
    public string menuMusic;
    //Versus Music
    public string versusMusic;
  //
  //
  //


    private Queue<string> soundEventQueue = new Queue<string>();
    private bool isPlayingSound = false;
    private FMOD.Studio.EventInstance currentSoundInstance;
    //Fmod:
    private void EnqueueSoundEvent(string soundEventPath)
    {
        soundEventQueue.Enqueue(soundEventPath);
    }



    //Musics:
    public void PlayMenuMusic()
    {
        EnqueueSoundEvent(menuMusic);
    }
    public void PlayVersusMusic()
    {
        EnqueueSoundEvent(versusMusic);
    }

    //Efects:10
    private void Update()
    {
        if (!isPlayingSound)
        {
            PlayNextSoundEvent();
        }
        else
        {
            if (currentSoundInstance.isValid())
            {
                PLAYBACK_STATE playbackState;
                currentSoundInstance.getPlaybackState(out playbackState);

                if (playbackState != PLAYBACK_STATE.PLAYING)
                {
                    isPlayingSound = false;
                    currentSoundInstance.release();
                }
            }
        }
    }


    private void PlayNextSoundEvent()
    {
        if (soundEventQueue.Count > 0)
        {
            string soundEventPath = soundEventQueue.Dequeue();

            currentSoundInstance = RuntimeManager.CreateInstance(soundEventPath);
            currentSoundInstance.start();

            isPlayingSound = true;
        }
    }
}
/*
 
    #region Call Menu Sounds NoFMOD
    [Space(5)]
    [Header("Menu Sounds:")]
    public AudioMixer audioMixer;//preciso?
    public AudioSource audioSourcesMusicMenu;
    public AudioSource audioSourcesEffectsMenu;
    //
    public AudioClip menuMusicSound;
    public AudioClip menuSubmitSound;
    public AudioClip menuCancelSound;
    public AudioClip menuNavigateSound;
    //
    //Versus
    public AudioClip menuVersusMusicSound;
    //
    public AudioClip menuReadySound;
    public AudioClip menuAllReadySound;
    //
    public void Play_MenuMusicSound()
    {
        audioSourcesMusicMenu.clip = menuMusicSound;
        audioSourcesMusicMenu.loop = true;
        audioSourcesMusicMenu.Play();
      //audioSources.PlayClipAtPoint(menuMusicSound, this.transform.position);
      //  menuMusicSound.Play();
    }
    public void Play_MenuSubmitSound()
    {
        //  menuSubmitSound.Play();
        audioSourcesEffectsMenu.clip = menuSubmitSound;
        audioSourcesEffectsMenu.loop = false;
        audioSourcesEffectsMenu.Play();
    }
    public void Play_MenuCancelSound()
    {
        // menuCancelSound.Play();
        audioSourcesEffectsMenu.clip = menuCancelSound;
        audioSourcesEffectsMenu.loop = false;
        audioSourcesEffectsMenu.Play();
    }
    public void Play_MenuNavigateSound()
    {
        // menuNavigateSound.Play();
        audioSourcesEffectsMenu.clip = menuNavigateSound;
        audioSourcesEffectsMenu.loop = false;
        audioSourcesEffectsMenu.Play();
    }
    //
    //Versus
    public void Play_MenuVersusMusicSound()
    {
        //menuVersusMusicSound.Play();
        audioSourcesMusicMenu.clip = menuVersusMusicSound;
        audioSourcesMusicMenu.loop = true;
        audioSourcesMusicMenu.Play();
    }

    public void Play_MenuReadySound()
    {
        //   menuReadySound.Play();
        audioSourcesEffectsMenu.clip = menuReadySound;
        audioSourcesEffectsMenu.loop = false;
        audioSourcesEffectsMenu.Play();
    }
    public void Play_MenuAllReadySound()
    {
        //   menuAllReadySound.Play();
        audioSourcesEffectsMenu.clip = menuAllReadySound;
        audioSourcesEffectsMenu.loop = false;
        audioSourcesEffectsMenu.Play();
    }


    #endregion


 */