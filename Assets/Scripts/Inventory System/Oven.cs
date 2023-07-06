using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Oven : MonoBehaviour
{
    public MeltingOven meltingOven;

    public Item ore;
    public Item stone;

    public Image oreIcon;
    public Image stoneIcon;

    public TMP_Text oreAmount;
    public TMP_Text stoneAmount;

    public Item rawItem;
    public Image rawIcon;
    public TMP_Text rawAmount;

    public Item resultItem;
    public Image resultIcon;
    public TMP_Text resultAmount;

    private void OnEnable()
    {
        resultIcon.gameObject.SetActive(false);
        resultAmount.gameObject.SetActive(false);

        rawIcon.gameObject.SetActive(false);
        rawAmount.gameObject.SetActive(false);

        StartCoroutine(Go());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Go()
    {
        bool run = true;

        while(run)
        {
            if(meltingOven != null)
            {
                if(meltingOven.processedItem != null)
                {
                    resultIcon.gameObject.SetActive(true);
                    resultAmount.gameObject.SetActive(true);

                    rawIcon.gameObject.SetActive(true);
                    rawAmount.gameObject.SetActive(true);

                    resultItem = meltingOven.processedItem;
                    resultIcon.sprite = resultItem.icon;
                    resultAmount.text = meltingOven.processedAmount.ToString();

                    rawItem = meltingOven.rawResource;
                    rawIcon.sprite = rawItem.icon;
                    rawAmount.text = meltingOven.rawAmount.ToString();
                }
                else if(meltingOven.processedItem == null)
                {
                    resultIcon.gameObject.SetActive(false);
                    resultAmount.gameObject.SetActive(false);

                    rawIcon.gameObject.SetActive(false);
                    rawAmount.gameObject.SetActive(false);
                }

                ore = meltingOven.metal;
                stone = meltingOven.stone;

                oreIcon.sprite = ore.icon;
                stoneIcon.sprite = stone.icon;

                oreAmount.text = meltingOven.inventoryManager.GetTotalAmount(ore).ToString();
                stoneAmount.text = meltingOven.inventoryManager.GetTotalAmount(stone).ToString();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void Metal()
    {
        meltingOven.PutItemInOvenMetal();
    }

    public void Stone()
    {
        meltingOven.PutItemInOvenStone();
    }

    public void PickUp()
    {
        meltingOven.GetProcessedResources();
    }
}
