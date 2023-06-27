using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting", menuName = "Crafting", order = 3)]
public class Crafting : ScriptableObject
{
    [System.Serializable]
    public struct Hello
    {
        public int hi;
        public float de;
    }

    public Hello[] hello;
}
