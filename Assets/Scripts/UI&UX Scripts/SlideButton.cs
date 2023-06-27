using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlideButton : MonoBehaviour
{
    public ScreenManager screenManager;

    public int currentslide;
    public float slideSpeed;
    public Vector3 startPos, targetPos, endPos;

    [System.Serializable]
    public struct Resolution
    {
        public int width;
        public int height;
        public RectTransform slideTransform;
        public TMP_Text text;
    }

    public Resolution[] resolution;

    void Start()
    {
        for(int i = 0; i < resolution.Length; i++)
        {
            if(resolution[i].text != null)
                resolution[i].text.text = $"{resolution[i].width} x {resolution[i].height}";
        }

        for(int i =0; i< resolution.Length - 1; i++)
        {
            if(resolution[i].slideTransform.localPosition == targetPos)
            {
                currentslide = i;
                return;
            }
        }
    }

    public void ButtonSlideWin(int increment)
    {
        int targetSlide = currentslide - increment;

        if(targetSlide > resolution.Length - 1 || targetSlide < 0)
            return;

        Vector3 currentTargetPos = Vector3.zero;

        if(targetSlide < currentslide)
            currentTargetPos = endPos;
        else if(targetSlide > currentslide)
            currentTargetPos = startPos;

        if(screenManager.currentWindowMode == 1)
            screenManager.Resolution(Screen.currentResolution.width, Screen.currentResolution.height, 0);
        else
            screenManager.Resolution(Screen.currentResolution.width, Screen.currentResolution.height, 1);

        StartCoroutine(SettingsAnimation(targetSlide, currentTargetPos));
    }

    public void ButtonSlideReso(int increment)
    {
        int targetSlide = currentslide - increment;

        if(targetSlide > resolution.Length - 1 || targetSlide < 0)
            return;

        Vector3 currentTargetPos = Vector3.zero;

        if(targetSlide < currentslide)
            currentTargetPos = endPos;
        else if(targetSlide > currentslide)
            currentTargetPos = startPos;

        screenManager.Resolution(resolution[targetSlide].width, resolution[targetSlide].height, screenManager.currentWindowMode);


        StartCoroutine(SettingsAnimation(targetSlide, currentTargetPos));
    }

    IEnumerator SettingsAnimation(int targetSlide, Vector3 endPos)
    {
        bool runCoroutine = true;
        int current = currentslide;

        while(runCoroutine)
        {
            resolution[targetSlide].slideTransform.localPosition = Vector2.MoveTowards(resolution[targetSlide].slideTransform.localPosition, targetPos, slideSpeed * Time.deltaTime);
            resolution[current].slideTransform.localPosition = Vector2.MoveTowards(resolution[current].slideTransform.localPosition, endPos, slideSpeed * Time.deltaTime);

            if(resolution[targetSlide].slideTransform.localPosition == targetPos && resolution[current].slideTransform.localPosition == endPos)
            {
                currentslide = targetSlide;
                runCoroutine = false;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
