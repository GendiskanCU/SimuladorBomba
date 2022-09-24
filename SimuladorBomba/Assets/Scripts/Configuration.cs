using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{
    [SerializeField] [Tooltip("Barra de ajuste de volumen de los SFX")]
    private Scrollbar volumeSFXBar;

    [SerializeField] [Tooltip("Barra de ajuste de volumen de la música")]
    private Scrollbar volumeMusicBar;

    [SerializeField] [Tooltip("Caja de texto de los segundos totales")]
    private TMP_InputField secondsText;

    private void OnEnable()
    {
        volumeSFXBar.value = PlayerPrefs.GetFloat("SFX_VOLUME");
        volumeMusicBar.value = PlayerPrefs.GetFloat("AMBIANCE_VOLUME");
        secondsText.text = PlayerPrefs.GetFloat("TOTAL_SECONDS").ToString();
    }

    public void OnIncreaseButton()
    {
        float currentValue = float.Parse(secondsText.text);
        if (currentValue < 999f)
        {
            currentValue++;
            secondsText.text = currentValue.ToString();
        }
    }
    
    public void OnDecreaseButton()
    {
        float currentValue = float.Parse(secondsText.text);
        if (currentValue > 1f)
        {
            currentValue--;
            secondsText.text = currentValue.ToString();
        }
    }
    
}
