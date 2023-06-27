using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slider : MonoBehaviour
{
    public ScreenManager screenManager;

    public UnityEngine.UI.Slider slider;
    public TMP_Text text;

    public bool fov;
    public bool fps;
    public bool Mouse;

    void Start()
    {
        //slider.value = screenManager.targetFPS;
        //DisplayTextFPS();
    }

    public void DisplayTextFPS()
    {
        text.text = slider.value.ToString();
        screenManager.FPSTarget((int)slider.value);
    }

    public void DisplayMouse()
    {
        if(slider.value > 0.1f)
            text.text = (Mathf.Round(slider.value * 100.0f) * 0.01f).ToString();
        else if(slider.value <= 0.1f)
            text.text = "0.1";
    }
}
