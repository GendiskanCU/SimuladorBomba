using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] [Tooltip("Texto del cronómetro")]
    private TextMeshProUGUI timerText;

    [SerializeField] [Tooltip("Imagen del panel de estado del temporizador")]
    private ImageFadeOut statusPanel;

    [SerializeField] [Tooltip("Imagen con el panel de configuración del temporizador")]
    private Image configurationPanel;

    [SerializeField] [Tooltip("Imagen con el efecto de fin de tiempo y explosión final")]
    private Image endOfTimeEffect;

    [SerializeField] [Tooltip("Sonido del temporizador")]
    private AudioClip timerSound;
    
    [SerializeField] [Tooltip("Sonido de la explosión")]
    private AudioClip explosionSound;

    [SerializeField] [Tooltip("Sonido botón de Stop")]
    private AudioClip stopSound;

    //Segundos de duración del temporizador
    private float totalSeconds;

    //Minutos, segundos y centésimas que se mostrarán en la pantalla
    private int minutes, seconds, hundredths;
    
    //Para controlar si han cambiado los segundos
    private int oldSeconds;

    private float timeElapsed;

    private bool timerOn;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("TOTAL_SECONDS"))
        {
            PlayerPrefs.SetFloat("TOTAL_SECONDS", 30f);
        }
        SetInitialTime();
    }


    private void FixedUpdate()
    {
        if (timerOn)
        {
            totalSeconds -= Time.fixedDeltaTime;
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        oldSeconds = seconds;
        
        minutes = Mathf.FloorToInt(totalSeconds / 60);
        seconds = Mathf.FloorToInt(totalSeconds % 60);

        if (timerOn && seconds != oldSeconds)
        {
            SoundManager.SharedInstance.PlayEffectSound(timerSound);
        }
        
        hundredths = Mathf.FloorToInt((totalSeconds - Mathf.FloorToInt(totalSeconds)) * 100f);
        
        if (totalSeconds <= 0)
        {
            SetExplosion();
        }

        WriteTimerText();
    }

    private void WriteTimerText()
    {
        timerText.text =
            $"{minutes.ToString("00")}:{seconds.ToString("00")}:{hundredths.ToString("00")}";
    }

    
    public void OnStartButton()
    {
        if (!timerOn)
        {
            timerText.gameObject.SetActive(true);
            StartCoroutine(statusPanel.SetFadeOut());
            Invoke("ChangeTimerStatus", 1f);
            SoundManager.SharedInstance.PlayAmbianceSound();
        }
    }

    public void OnExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }


    public void OnConfigurationButton()
    {
        if (!timerOn)
        {
            configurationPanel.gameObject.SetActive(true);
        }
    }

    public void OnStopButton()
    {
        if (timerOn)
        {
            SoundManager.SharedInstance.StopAmbianceSound();
            SoundManager.SharedInstance.PlayEffectSound(stopSound);
            ResetTimer();
        }
    }
    

    private void ChangeTimerStatus()
    {
        timerOn = !timerOn;
    }

    private void SetExplosion()
    {
        timerOn = false;
        minutes = 0;
        seconds = 0;
        hundredths = 0;
        
        SoundManager.SharedInstance.StopAmbianceSound();
        endOfTimeEffect.gameObject.SetActive(true);
        SoundManager.SharedInstance.SetVolumeEffects(1f);
        SoundManager.SharedInstance.PlayEffectSound(explosionSound);
        
        Invoke("ResetTimer", 8f);
    }


    public void SetInitialTime()
    {
        timerOn = false;
        totalSeconds = PlayerPrefs.GetFloat("TOTAL_SECONDS");
        UpdateTimer();
        WriteTimerText();
    }
    
    private void ResetTimer()
    {
        endOfTimeEffect.gameObject.SetActive(false);
        SoundManager.SharedInstance.SetVolumeEffects(PlayerPrefs.GetFloat("SFX_VOLUME"));
        timerText.gameObject.SetActive(false);
        statusPanel.Reset();
        SetInitialTime();
    }
}


