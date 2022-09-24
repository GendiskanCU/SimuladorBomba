using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeOut : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }


    public IEnumerator SetFadeOut()
    {
        while (_image.color.a > 0f)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b,
                _image.color.a - 0.1f);
            
            yield return new WaitForSeconds(0.1f);
        }
        
        yield break;
    }

    public void Reset()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
    }
}
