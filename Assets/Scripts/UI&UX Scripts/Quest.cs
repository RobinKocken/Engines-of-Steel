using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject finishedUI;

    public void IsFinished()
    {
        finishedUI.SetActive(true);
    }
}
