using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DonutClicker/StoreSettings", order = 1)]
public class StoreSettings : ScriptableObject
{
    public List<StoreItem> StoreItems = new List<StoreItem>();

    public int GetIndexByName(string itemName)
    {
        for (int i = 0; i < StoreItems.Count; i++)
        {
            if (string.Equals(StoreItems[i].Name, itemName))
                return i;
        }

        return 0;
    }
}