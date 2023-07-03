using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    public CraftingTable craftingTable;

    public Crafting crafting;
    public int[] currentAmount;

    void Start()
    {
        currentAmount = new int[crafting.requirements.Length];
    }

    public void SelectCraft()
    {
        for(int i = 0; i < crafting.requirements.Length; i++)
        {
            currentAmount[i] = craftingTable.CheckIfCanCraft(crafting.requirements[i].requiredItem);
            craftingTable.SelectCraft(crafting, currentAmount);
        }
    }
}
