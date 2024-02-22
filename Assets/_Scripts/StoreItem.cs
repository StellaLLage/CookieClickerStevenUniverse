using System;
using System.Collections.Generic;
using UnityEngine;

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