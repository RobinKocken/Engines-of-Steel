using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSlot : MonoBehaviour
{
    public CraftingTable craftingTable;
    public Image icon;
    public TMP_Text amount;

    public Crafting crafting;
    public int[] currentAmount;

    void Start()
    {
        currentAmount = new int[crafting.requirements.Length];

        icon.sprite = crafting.resultItem.icon;
        amount.text = crafting.resultAmount.ToString();
    }

    public void SelectCraft()
    {
        for(int i = 0; i < crafting.requirements.Length; i++)
        {
            currentAmount[i] = craftingTable.CheckIfCanCraft(crafting.requirements[i].requiredItem);
            craftingTable.SelectCraft(crafting, currentAmount);

        }
            Debug.Log(crafting.resultItem.itemName);
    }
}
