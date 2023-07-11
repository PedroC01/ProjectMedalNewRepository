using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class SoundManagerScript : MonoBehaviour
{
    [Header("SoundsForMedaParts")]
    public string HeadCritDamage;
    public string LeftArmDestroyed;
    public string RightArmDestroyed;
    public string LegsDestroyed;

    private Queue<string> soundEventQueue = new Queue<string>();
    private bool isPlayingSound = false;
    private FMOD.Studio.EventInstance currentSoundInstance;

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

    public void PlayHeadCritDamageSound()
    {
        EnqueueSoundEvent(HeadCritDamage);
    }

    public void PlayLeftArmDestroyedSound()
    {
        EnqueueSoundEvent(LeftArmDestroyed);
    }

    public void PlayRightArmDestroyedSound()
    {
        EnqueueSoundEvent(RightArmDestroyed);
    }

    public void PlayLegsDestroyedSound()
    {
        EnqueueSoundEvent(LegsDestroyed);
    }

    private void EnqueueSoundEvent(string soundEventPath)
    {
        soundEventQueue.Enqueue(soundEventPath);
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