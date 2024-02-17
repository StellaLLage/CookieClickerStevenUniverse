using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "DonutClicker/StoreSettings", order = 1)]
public class StoreSettings : ScriptableObject
{
    public List<StoreItem> StoreItems = new List<StoreItem>();
}

[Serializable]
public class StoreItem
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Sprite Icon;
    [SerializeField] public int PurchaseCost;
    [SerializeField] public List<int> UpgradeCost;
    [SerializeField] public float Multiplier;
    [SerializeField] public int MaxLevel;

    public int GetCurrentUpgradeCost(int currentLevel)
    {
        return currentLevel > UpgradeCost.Count ? UpgradeCost[^1] : UpgradeCost[currentLevel];
    }
}
