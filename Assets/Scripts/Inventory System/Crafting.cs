using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting", menuName = "Crafting", order = 3)]
public class Crafting : ScriptableObject
{
    public Requirements[] requirements;

    [System.Serializable]
    public struct Requirements
    {
        public Item requiredItem;
        public int requiredAmount;
    }

    public Item resultItem;
    public int resultAmount;
}
