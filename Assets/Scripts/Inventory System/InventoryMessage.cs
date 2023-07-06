using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMessage : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;


    void Start()
    {
        StartCoroutine(MessageAnimation());   
    }

    IEnumerator MessageAnimation()
    {
        bool run = true;

        while(run)
        {


            if(transform.localPosition == endPos)
            {


            }
        }


        yield return null;
    }
}
