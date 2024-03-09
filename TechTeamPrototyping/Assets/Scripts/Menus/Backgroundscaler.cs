using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Backgroundscaler : MonoBehaviour
{
    UnityEngine.UI.Image backgroundImage;
    RectTransform rt;
    float ratio;

    
    void Start()
    {
        backgroundImage = GetComponent< UnityEngine.UI.Image>();
        rt = backgroundImage.rectTransform;
        ratio = backgroundImage.sprite.bounds.size.x / backgroundImage.sprite.bounds.size.y;
    }

    
    void Update()
    {
        if (!rt)
            return;

        //Scale image proportionally to fit the screen dimensions, while preserving aspect ratio
        if(Screen.height * ratio >= Screen.width)
        {
            rt.sizeDelta = new Vector2(Screen.height * ratio, Screen.height);
        }
        else
        {
            rt.sizeDelta = new Vector2(Screen.width, Screen.width / ratio);
        }
    }
}
