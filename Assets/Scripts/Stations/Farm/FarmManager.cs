using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FarmManager : MonoBehaviour
{
    public GameManager gameManager;
    public UnityEngine.UIElements.Button button;
    private FarmController currentFarm;
    public GameObject farmUI;
    public GameObject progressUI;
    public UnityEngine.UI.Slider progressSlider;
    public GameObject cropListUI;
    public GameObject harvestUI;

    public void SelectCrop(int cropIndex)
    {
        currentFarm.PlantCrop(cropIndex);
    }
    public void Harvest()
    {
        currentFarm.HarvestCrops();
    }
}
