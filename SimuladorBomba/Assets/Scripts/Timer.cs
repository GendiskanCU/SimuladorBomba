using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [SerializeField] [Tooltip("Texto del cronómetro")]
    private TextMeshProUGUI timerText;

    //Minutos, segundos y centésimas de segundo
    private int minutes, seconds;


    private void Start()
    {
        minutes = 1;
        seconds = 30;
        WriteTimerText();
    }

    private void Update()
    {
        
    }


    private IEnumerator UpdateTimer()
    {
        while (minutes != 0 || seconds != 0)
        {
            if (seconds > 0)
            {
                seconds--;
                WriteTimerText();
            }

            if (seconds == 0 && minutes > 0)
            {
                yield return new WaitForSecondsRealtime(1f);
                seconds = 59;
                minutes--;
                WriteTimerText();
            }

            yield return new WaitForSecondsRealtime(1f);
        }

    }
    

    private void WriteTimerText()
    {
        timerText.text =
            $"{minutes.ToString("00")}:{seconds.ToString("00")}";
    }


    
    public void OnStartButton()
    {
        StartCoroutine(UpdateTimer());
    }
}


