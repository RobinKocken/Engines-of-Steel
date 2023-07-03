using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public enum Table
    {
        modulaire,
        probs,
        Workstation,
    }
    public Table table;

    public Crafting currentCrafting;
    public int[] currentAmount = new int[3];

    public int CheckIfCanCraft(Item item)
    {
        int amount = inventoryManager.GetTotalAmount(item);
        return amount;
    }

    public void SelectCraft(Crafting crafting ,int[] amount)
    {
        currentCrafting = crafting;
        currentAmount = amount;
    }

    public void Craft()
    {
        for(int i = 0; i < currentCrafting.requirements.Length; i++)
        {
            if(currentAmount[i] < currentCrafting.requirements[i].requiredAmount)
            {
                Debug.Log("Can't");
                return;
            }
        }

        for(int i = 0; i < currentCrafting.requirements.Length; i++)
        {
            inventoryManager.RemoveItem(currentCrafting.requirements[i].requiredItem, currentCrafting.requirements[i].requiredAmount, -1);
        }

        inventoryManager.AddItem(currentCrafting.resultItem, currentCrafting.resultAmount, -1);
        Debug.Log("Crafted");
    }

}
