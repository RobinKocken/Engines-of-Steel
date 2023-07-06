using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject stationFollow;

    public GameObject stationIcon;

    private void OnEnable()
    {
        float xPos = stationFollow.transform.localPosition.x * 0.09f;
        float yPos = stationFollow.transform.localPosition.z * 0.09f;

        stationIcon.transform.localPosition = new Vector3(xPos, yPos, 0);
        stationIcon.transform.localEulerAngles = new Vector3(0, 0, -stationFollow.transform.eulerAngles.y);
    }
}
