using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject startUI;
    public GameObject cred;

    private void Start()
    {
        cred.SetActive(false);
    }

    public void CreditsOn()
    {
        cred.SetActive(true);
        startUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            cred.SetActive(false);
            startUI.SetActive(true);
        }

    }
}
