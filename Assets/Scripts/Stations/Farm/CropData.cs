using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "CropData", order = 2)]
public class CropData : ScriptableObject
{
    public Item item;
    public int CropID;
    public GameObject[] GrowthStages;
    public int timeToGrow;
    
}
