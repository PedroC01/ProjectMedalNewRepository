using UnityEngine;
using System;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;


public class SoundManagerScript : MonoBehaviour
{
    [Header("SoundsForMedaParts")]
    public string HeadCritDamage;
    private FMOD.Studio.EventInstance HeadCritSoundInstance;
    public string LeftArmDestroyed;
    private FMOD.Studio.EventInstance LArmDSoundInstance;
    public string RightArmDestroyed;
    private FMOD.Studio.EventInstance RArmDSoundInstance;
    public string LegsDestroyed;
    private FMOD.Studio.EventInstance LegsDSoundInstance;
    private FMOD.Studio.EventInstance currentSoundInstance;

    private Queue<string> soundEventQueue = new Queue<string>();
    private bool isPlayingSound = false;

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

        if (!isPlayingSound)
        {
            PlayNextSoundEvent();
        }
    }

    private void PlayNextSoundEvent()
    {
        if (soundEventQueue.Count > 0)
        {
            string soundEventPath = soundEventQueue.Dequeue();

            if (currentSoundInstance.isValid())
            {
                currentSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                currentSoundInstance.release();
            }

            currentSoundInstance = RuntimeManager.CreateInstance(soundEventPath);
            currentSoundInstance.start();

            isPlayingSound = true;
        }
        else
        {
            isPlayingSound = false;
        }
    }
}
