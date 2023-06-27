using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHolder : MonoBehaviour
{
    public Quest quest;

    public GameObject questTargetObject;

    private void OnDestroy()
    {
        quest.isFinished = true;
    }
}
