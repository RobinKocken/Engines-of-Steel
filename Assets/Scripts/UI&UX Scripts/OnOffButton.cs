using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButton : MonoBehaviour
{
    public ScreenManager screenManager;

    public GameObject onSprite;
    public GameObject offSprite;
    public bool isActive;

    void Start()
    {
        SwitchSprite(false);
    }

    public void Button()
    {
        isActive = !isActive;
        SwitchSprite(isActive);
    }

    void SwitchSprite(bool active)
    {
        onSprite.SetActive(active);
        offSprite.SetActive(!active);

        if(active)
            screenManager.VSync(1);
        else if(!active)
        {
            screenManager.VSync(0);
            screenManager.FPSTarget(screenManager.targetFPS);
        }
    }
}
