using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public OptionManager optionManager;

    public int targetFPS;
    public int currentWindowMode;

    public Slider fpsSlider;
    public Slider mouseSlider;

    void Start()
    {
        Resolution(1920, 1080, 0);

        FPSTarget(targetFPS);
        VSync(0);

        fpsSlider.DisplayTextFPS();
        mouseSlider.DisplayMouse();
    }

    private void Update()
    {
        optionManager.mouseSens = mouseSlider.slider.value;
    }

    public void Resolution(int width, int height, int windowMode)
    {
        switch(windowMode)
        {
            case 0:
            {
                Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
                currentWindowMode = 0;

                break;
            }
            case 1:
            {
                Screen.SetResolution(width, height, FullScreenMode.Windowed);
                currentWindowMode = 1;

                break;
            }
        } 
    }

    public void FPSTarget(int fps)
    {
        Application.targetFrameRate = fps;
        targetFPS = fps;
    }

    public void VSync(int vSync)
    {
        QualitySettings.vSyncCount = vSync;
    }
}
