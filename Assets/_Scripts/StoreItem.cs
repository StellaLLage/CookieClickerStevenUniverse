using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreItem : Item
{
    [SerializeField] public string Description;
    [SerializeField] public int PurchaseCost;
    [SerializeField] public List<int> UpgradeCost;

    public int GetCurrentUpgradeCost(int currentLevel)
    {
        return currentLevel > UpgradeCost.Count ? UpgradeCost[^1] : UpgradeCost[currentLevel];
    }

    public StoreItem(string name, Sprite icon, float multiplier, int maxLevel, string description, int purchaseCost, List<int> upgradeCost) : base(name, icon, multiplier, maxLevel)
    {
        Description = description;
        PurchaseCost = purchaseCost;
        UpgradeCost = upgradeCost;
    }
}

public class Item
{
    [SerializeField] public string Name;
    [SerializeField] public Sprite Icon;
    [SerializeField] public float Multiplier;
    [SerializeField] public int MaxLevel;

    public Item(string name, Sprite icon, float multiplier, int maxLevel)
    {
        Name = name;
        Icon = icon;
        Multiplier = multiplier;
        MaxLevel = maxLevel;
    }
}

public class ShowcaseItem : Item
{
    [SerializeField] public int CurrentLevel;

    public ShowcaseItem(string name, Sprite icon, float multiplier, int maxLevel, int currentLevel) : base(name, icon, multiplier, maxLevel)
    {
        CurrentLevel = currentLevel;
    }
}