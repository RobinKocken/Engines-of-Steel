using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMessage : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public float animationSpeed;

    public Image icon;
    public TMP_Text itemName;
    public TMP_Text amount;

    public void StartMessage(Item item, int amount)
    {
        transform.localPosition = startPos;

        icon.sprite = item.icon;
        itemName.text = item.itemName;
        this.amount.text = amount.ToString();

        StartCoroutine(MessageAnimation());
    }

    IEnumerator MessageAnimation()
    {
        bool run = true;

        while(run)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, endPos, animationSpeed * Time.deltaTime);

            if(transform.localPosition == endPos)
            {
                run = false;

                Destroy(gameObject);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
