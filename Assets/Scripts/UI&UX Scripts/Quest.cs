using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject finishedUI;

    private void Start()
    {
        finishedUI.SetActive(false);
    }

    public void IsFinished()
    {
        finishedUI.SetActive(true);
    }
}
