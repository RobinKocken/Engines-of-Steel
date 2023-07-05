using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public Image craftIcon;
    public TMP_Text craftAmount;

    public Crafting currentCrafting;
    public int[] currentAmount = new int[3];

    public ItemNeed[] itemNeed;
    public CraftTopUI topUI;

    public int CheckIfCanCraft(Item item)
    {
        int amount = inventoryManager.GetTotalAmount(item);
        return amount;
    }

    public void SelectCraft(Crafting crafting ,int[] amount)
    {
        currentCrafting = crafting;
        currentAmount = amount;

        craftIcon.gameObject.SetActive(false);
        craftAmount.gameObject.SetActive(false);

        for(int i = 0; i < itemNeed.Length; i++)
        {
            itemNeed[i].icon.gameObject.SetActive(false);
            itemNeed[i].amountNeed.gameObject.SetActive(false);
            itemNeed[i].amountHave.gameObject.SetActive(false);
            itemNeed[i].red.SetActive(false);
        }

        craftIcon.sprite = currentCrafting.resultItem.icon;
        craftAmount.text = currentCrafting.resultAmount.ToString();

        craftIcon.gameObject.SetActive(true);
        craftAmount.gameObject.SetActive(true);

        for(int i = 0; i < amount.Length; i++)
        {
            itemNeed[i].icon.sprite = currentCrafting.requirements[i].requiredItem.icon;
            itemNeed[i].icon.gameObject.SetActive(true);

            itemNeed[i].amountNeed.text = currentCrafting.requirements[i].requiredAmount.ToString();
            itemNeed[i].amountNeed.gameObject.SetActive(true);
            itemNeed[i].amountHave.text = currentAmount[i].ToString();
            itemNeed[i].amountHave.gameObject.SetActive(true);

            if(currentAmount[i] < currentCrafting.requirements[i].requiredAmount)
                itemNeed[i].red.SetActive(true);
        }
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

    IEnumerator BarAnimation(int targetIndex)
    {
        topUI.coroutineRun = true;

        topUI.back[topUI.currentTargetIndex].gameObject.SetActive(false);
        topUI.back[targetIndex].gameObject.SetActive(true);

        topUI.currentTargetIndex = targetIndex;

        while(topUI.coroutineRun)
        {
            topUI.bar.localPosition = Vector2.MoveTowards(topUI.bar.localPosition, topUI.barTarget[targetIndex].localPosition, topUI.barSpeed * Time.deltaTime);
            topUI.bar.sizeDelta = Vector2.MoveTowards(topUI.bar.sizeDelta, topUI.barTarget[targetIndex].sizeDelta, topUI.barSpeed * Time.deltaTime);



            if(topUI.bar.localPosition == topUI.barTarget[targetIndex].localPosition && topUI.bar.sizeDelta == topUI.barTarget[targetIndex].sizeDelta)
            {
                topUI.coroutineRun = false;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

[System.Serializable]
public class ItemNeed 
{
    public Image icon;
    public GameObject red;
    public TMP_Text amountNeed;
    public TMP_Text amountHave;
}

[System.Serializable]
public class CraftTopUI
{
    public RectTransform bar;
    public Color barColor;
    public float barSpeed;

    public int currentTargetIndex;

    public bool coroutineRun;

    public RectTransform[] barTarget;
    public RectTransform[] back;
    public Color backColor;
}
