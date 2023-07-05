using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public MeltingOven meltingOven;

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
