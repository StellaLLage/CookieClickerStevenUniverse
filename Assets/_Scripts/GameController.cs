using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float addDonut = 0;
    private int _currentDonuts;

    [SerializeField] private DonutUIContainer _donutUIContainer;
    private List<ShowcaseItem> _activeMultipliers = new List<ShowcaseItem>();

    public List<ShowcaseItem> ActiveMultipliers => _activeMultipliers;

    public float CurrentMultiplier => GetCurrentMultiplier();

    public int CurrentDonuts
    {
        get => _currentDonuts;
        set
        {
            _currentDonuts = value;
            OnDonutsUpdated?.Invoke(_currentDonuts);
        }
    }
    
    public event Action<int> OnDonutsUpdated;
    public event Action<float> OnMultipliersUpdated;
    public event Action<ShowcaseItem> OnShowcaseItemSold;
    
    private void Awake()
    {
        GameManager.Instance.SetGameController(this);
        _donutUIContainer.OnDonutClicked += OnDonutClicked;
    }
    
    private void Start()
    {
        InvokeRepeating("AddDonutsEverySecond", 1, 1);
    }
    
    private void AddDonutsEverySecond()
    {
        if (this == null)
            return; 
        
        if (ActiveMultipliers.Count <= 0)
            return;
        
        addDonut += CurrentMultiplier;

        if ((int)addDonut <= 0)
            return;
        
        AddDonut((int)addDonut);
        addDonut = 0;
    }

    
    private float GetCurrentMultiplier()
    {
        float activeMultiplier = 0;
        foreach (var currentMultiplier in _activeMultipliers)
            activeMultiplier += currentMultiplier.Multiplier;

        return activeMultiplier;
    }

    private void OnDonutClicked()
    {
        //current donuts += 1 * multiplier de click
        AddDonut(1);
    }
    
    private void AddDonut(int donutAmount)
    {
        CurrentDonuts += donutAmount;
    }

    private void RemoveDonut(int donutAmount)
    {
        CurrentDonuts -= donutAmount;
    }
    
    public void OnStoreItemUpdated(ShowcaseItem item, int cost)
    {
        TryAddOrUpdateMultipliers(item);
        RemoveDonut(cost);
    }
    
    private void TryAddOrUpdateMultipliers(ShowcaseItem item)
    {
        if (ContainsItemOnActiveMultipliers(item))
            UpdateActiveMultiplier(item);
        else
            CreateNewMultiplier(item);
        
        OnMultipliersUpdated?.Invoke(CurrentMultiplier);
    }
    
    private bool ContainsItemOnActiveMultipliers(ShowcaseItem item)
    {
        foreach (var showcaseItem in _activeMultipliers)
        {
            if (string.Equals(showcaseItem.Name, item.Name))
                return true;
        }
        
        return false;
    }
    
    private int GetIndexOfItemOnActiveModifiers(ShowcaseItem item)
    {
        var cachedActiveMultipliers = _activeMultipliers;
        for (int i = 0; i < cachedActiveMultipliers.Count; i++)
        {
            if (string.Equals(cachedActiveMultipliers[i].Name, item.Name))
                return i;
        }
        return _activeMultipliers.IndexOf(item);
    }
    
    private void UpdateActiveMultiplier(ShowcaseItem item)
    {
        var index = GetIndexOfItemOnActiveModifiers(item);
        var upgradedMultiplierValue = item.Multiplier;
        upgradedMultiplierValue += _activeMultipliers[index].Multiplier;
        
        _activeMultipliers[index].Multiplier = upgradedMultiplierValue;
        _activeMultipliers[index].CurrentLevel = item.CurrentLevel;
        
        Debug.Log($"Updated active multiplier - Name: {item.Name} - New Level {_activeMultipliers[index].CurrentLevel} - New Multiplier {_activeMultipliers[index].Multiplier}");
    }
    
    private void CreateNewMultiplier(ShowcaseItem item)
    {
        _activeMultipliers.Add(item);
        
        var index = GetIndexOfItemOnActiveModifiers(item);
        Debug.Log($"Created new multiplier - Name: {item.Name} - Starter Multiplier {_activeMultipliers[index].Multiplier}");
    }
    
    public void OnItemSold(ShowcaseItem itemSold)
    {
        Debug.Log($"TRYING REFUND - Old Donut {CurrentDonuts} - old multiplier {CurrentMultiplier} ");
        
        AddDonut(GetTotalRefund(itemSold));
        RemoveMultiplier(itemSold);
        
        Debug.Log($"REFUND - New Donut {CurrentDonuts} - new multiplier {CurrentMultiplier}");
        
        OnMultipliersUpdated?.Invoke(CurrentMultiplier);
        OnShowcaseItemSold?.Invoke(itemSold);
    }

    private int GetTotalRefund(ShowcaseItem itemSold)
    {
        int totalRefund = GameManager.Instance.StoreController.GetItemPurchaseCostByName(itemSold.Name);
        
        Debug.Log($"itemSold.CurrentLevel >= 0 {itemSold.CurrentLevel >= 0}");
        Debug.Log($"itemSold.Multiplier {itemSold.Multiplier} > GameManager.Instance.StoreController.GetItemMultiplierBaseByName(itemSold.Name) {GameManager.Instance.StoreController.GetItemMultiplierBaseByName(itemSold.Name)}");
        var wasUpgraded = itemSold.CurrentLevel >= 0 && itemSold.Multiplier > GameManager.Instance.StoreController.GetItemMultiplierBaseByName(itemSold.Name);
        if (wasUpgraded)
            totalRefund += GameManager.Instance.StoreController.GetItemCostByNameAndUpgradeLevel(itemSold.Name, itemSold.CurrentLevel);
        
        return totalRefund;
    }
    
    private void RemoveMultiplier(ShowcaseItem itemSold)
    {
        var showcaseItem = _activeMultipliers.SingleOrDefault(x => string.Equals(x.Name, itemSold.Name));
        if (showcaseItem != null)
            _activeMultipliers.Remove(showcaseItem);
    }
}