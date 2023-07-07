using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FarmController : MonoBehaviour, IInteractable
{
    public GameObject[] crops;
    public GameObject currentCrop;
    private int cropToGrow;
    public FarmManager farmManager;
    //will only update for each growth stage
    public float cropProgress;

    public int growthStage;
    public bool fullyGrown;
    private GameObject oldCrop;

    public void Start()
    {
        farmManager = gameObject.GetComponentInParent<FarmManager>();
    }
    //if the player interacts with a farm, tell the farm manager which farm the player wants to interact with
    public void Interact(GameManager gameManager)
    {
        GameObject curSlot = gameManager.inventoryManager.GetSelectedGameObject();
        if(currentCrop == null)
        {
            curSlot.TryGetComponent(out Crop cropToPlant);
            PlantCrop(cropToPlant.cropData.CropID);
            gameManager.inventoryManager.RemoveItemFromHotbar(1);
        }
        else if(currentCrop != null)
        {
            if(fullyGrown)
            {
                HarvestCrops();
            }
        }
    }


    //tell the farm wich crop to plant and place it in the world
    public void PlantCrop(int _cropToGrow)
    {
        currentCrop = Instantiate(crops[_cropToGrow], this.transform);
        currentCrop.transform.localPosition = new Vector3(0, 0.24f, 0);
        StartCoroutine(GrowCrop(currentCrop.GetComponent<Crop>()));
        oldCrop = currentCrop;
    }
    //first check if the crop is fully grown now, if so stop growing process, else wait for X seconds(X being the growth time divided by the total ammount of growth stages) 
    public IEnumerator GrowCrop(Crop _cropToGrow)
    {
        bool run = true;
        while(run)
        {
            if (growthStage == _cropToGrow.cropData.GrowthStages.Length - 1)
            {
                run = false;
                fullyGrown = true;
                //StopCoroutine(GrowCrop(_cropToGrow));
            }
            else
            {
                yield return new WaitForSeconds(_cropToGrow.cropData.timeToGrow / _cropToGrow.cropData.GrowthStages.Length);
                
                growthStage++;
                cropProgress = (growthStage / (float)currentCrop.GetComponent<Crop>().cropData.GrowthStages.Length) * 100;
                cropProgress += 100 / (float)currentCrop.GetComponent<Crop>().cropData.GrowthStages.Length;
                if (growthStage != _cropToGrow.cropData.GrowthStages.Length)
                {
                    Grow();
                }
            }
        }
    }
    //remove the old growth stage and place the new
    public void Grow()
    {
        if (oldCrop != null)
        {
            Destroy(oldCrop);
        }
        oldCrop = Instantiate(currentCrop.GetComponent<Crop>().cropData.GrowthStages[growthStage], this.transform);
        currentCrop = oldCrop;
    }
    //harvest the crop and put it in the inventory
    public void HarvestCrops()
    {
        Destroy(currentCrop);
        currentCrop = null;
        farmManager.gameManager.inventoryManager.AddItem(farmManager.gameManager.inventoryManager.itemHolders[cropToGrow+5], 4, -1);
    }

}
