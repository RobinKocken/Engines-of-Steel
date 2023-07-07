using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectFromQuest : MonoBehaviour
{
    public GameObject prefab;

    private void OnDestroy()
    {
        prefab.SetActive(true);
    }
}
