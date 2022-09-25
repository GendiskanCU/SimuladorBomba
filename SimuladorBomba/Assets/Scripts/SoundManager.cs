using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] [Tooltip("AudioSource de los efectos de sonido")]
    private AudioSource effectsSource;

    [SerializeField] [Tooltip("AudioSource de la música o sonido ambiente")]
    private AudioSource ambienceSource;
    
    //Singleton
    public static SoundManager SharedInstance;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }


    private void Start()
    {
        if (!PlayerPrefs.HasKey("SFX_VOLUME"))
        {
            PlayerPrefs.SetFloat("SFX_VOLUME", 0.2f);
        }
        if (!PlayerPrefs.HasKey("AMBIANCE_VOLUME"))
        {
            PlayerPrefs.SetFloat("AMBIANCE_VOLUME", 0.1f);
        }

        effectsSource.volume = PlayerPrefs.GetFloat("SFX_VOLUME");
        ambienceSource.volume = PlayerPrefs.GetFloat("AMBIANCE_VOLUME");
    }

    public void PlayEffectSound(AudioClip soundForPlay)
    {
        effectsSource.Stop();
        effectsSource.clip = soundForPlay;
        effectsSource.Play();
    }
    
    public void PlayAmbianceSound()
    {
        ambienceSource.Play();
    }

    public void StopAmbianceSound()
    {
        ambienceSource.Stop();
    }

    
    public void SetVolumeEffects(float newVolume)
    {
        float vol = 0f;

        if (newVolume > 1f)
            vol = 1f;
        else if (newVolume < 0f)
            vol = 0f;
        else
        {
            vol = newVolume;
        }
        
        effectsSource.volume = vol;
    }
    
    
    public void SetVolumeAmbiance(float newVolume)
    {
        float vol = 0f;

        if (newVolume > 1f)
            vol = 1f;
        else if (newVolume < 0f)
            vol = 0f;
        else
        {
            vol = newVolume;
        }
        
        ambienceSource.volume = vol;
    }

}
