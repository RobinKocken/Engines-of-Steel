using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeltingOven : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager;

    public Item processedItem;
    public int processedAmount;

    public Item rawResource;
    public int rawAmount;

    public Item metal;
    public Item stone;

    public Item steelResult;
    public Item brickResult;

    public float metalBurnTime;
    public float stoneBurnTime;

    public bool run;
    public bool bMetal;
    public bool bStone;

    public void Interact(GameManager gameManager)
    {
        gameManager.SwitchStatePlayer(GameManager.PlayerState.ui, UIManager.ExternalUIState.oven);

        inventoryManager = gameManager.inventoryManager;
        gameManager.uiManager.oven.meltingOven = this;
    }

    public void PutItemInOvenMetal()
    {
        if(!bStone)
        {
            int totalAmount = inventoryManager.GetTotalAmount(metal);

            if(totalAmount <= metal.maxStack - rawAmount)
            {
                rawAmount += totalAmount;
                inventoryManager.RemoveItem(metal, totalAmount, -1);
            }
            else if(rawAmount >= metal.maxStack - rawAmount)
            {
                int maxAmount = metal.maxStack - rawAmount;
                rawAmount += maxAmount;

                inventoryManager.RemoveItem(metal, maxAmount, -1);
            }

            if(!run && rawAmount > 0)
            {
                bMetal = true;
                run = true;
                StartCoroutine(Burner(metalBurnTime, steelResult));
            }
        }
    }

    public void PutItemInOvenStone()
    {
        if(!bMetal)
        {
            int totalAmount = inventoryManager.GetTotalAmount(stone);

            if(totalAmount <= metal.maxStack - rawAmount)
            {
                rawAmount += totalAmount;
                inventoryManager.RemoveItem(stone, totalAmount, -1);
            }
            else if(rawAmount >= metal.maxStack - rawAmount)
            {
                int maxAmount = metal.maxStack - rawAmount;
                rawAmount += maxAmount;

                inventoryManager.RemoveItem(stone, maxAmount, -1);
            }

            if(!run && rawAmount > 0)
            {
                bMetal = true;
                run = true;
                StartCoroutine(Burner(stoneBurnTime, brickResult));
            }
        }
    }

    public void GetProcessedResources()
    {
        if(processedAmount >= 1)
        {
            inventoryManager.AddItem(processedItem, processedAmount, -1);
            processedAmount -= processedAmount;

            if(processedAmount == 0 && rawAmount == 0)
            {
                bMetal = false;
                bStone = false;

                processedItem = null;

                Debug.Log("Set False");
            }
        }
    }

    IEnumerator Burner(float time, Item item)
    {
        processedItem = item;
        Debug.Log("Ie");

        while(run)
        {
            yield return new WaitForSeconds(time);

            rawAmount--;
            processedAmount++;

            if(rawAmount == 0)
            {
                Debug.Log("!run");
                run = false;
            }

        }
    }
}
