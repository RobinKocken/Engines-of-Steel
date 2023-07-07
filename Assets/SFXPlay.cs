using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlay : MonoBehaviour
{
    public AudioSource audioSource;

    public void ButtonSound()
    {
        audioSource.Play();
    }
}
