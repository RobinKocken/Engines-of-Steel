using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public UnityEngine.UI.Slider masterSlider;
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider sfxSlider;

    public TMP_Text masterText;
    public TMP_Text musicText;
    public TMP_Text sfxText;

    void Start()
    {
        //StartCoroutine(DelayStart());

        if(mixer != null)
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

            Debug.Log("Kkr");
        }


    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1);

        if(mixer == null)
        {
            Debug.Log("Kkr");
        }

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);

        masterText.text = ((int)(masterSlider.value * 100)).ToString();
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);

        musicText.text = ((int)(musicSlider.value * 100)).ToString();
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);

        sfxText.text = ((int)(sfxSlider.value * 100)).ToString();
    }
}
