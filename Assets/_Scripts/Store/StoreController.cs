using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] private StoreSettings _settings;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private StoreItemContainer _itemContainerRef;
    private List<StoreItemContainer> _storeItemContainers = new List<StoreItemContainer>();

    public StoreSettings Settings => _settings;

    public event Action<ShowcaseItem, int> OnPurchase;
    public event Action<ShowcaseItem, int> OnUpgrade;

    private void Awake()
    {
        GameManager.Instance.SetStoreController(this);
        GameManager.Instance.OnDonutsUpdated += OnDonutsUpdated;
        GameManager.Instance.OnShowcaseItemSold += OnShowcaseItemSold;
        SetupStore();
    }

    private void SetupStore()
    {
        var storeItems = _settings.StoreItems;
        var missingContainers = storeItems.Count - _storeItemContainers.Count;
        CreateMissingContainers(missingContainers);
        SetupItemContainers(storeItems);
    }

    private void CreateMissingContainers(int missingContainersValue)
    {
        for (int i = 0; i < missingContainersValue; i++)
        {
            var storeItem = Instantiate(_itemContainerRef, _itemsParent);
            _storeItemContainers.Add(storeItem);
        }
    }

    private void SetupItemContainers(List<StoreItem> storeItems)
    {
        for (int i = 0; i < _storeItemContainers.Count; i++)
        {
            var item = _storeItemContainers[i];
            
            if (i >= storeItems.Count)
                item.gameObject.SetActive(false);
            else
            {
                item.SetItem(storeItems[i]);
                item.ChangeActionButtonInteraction(GameManager.Instance.CurrentDonuts >= item.CurrentCost);
                item.OnPurchased += OnItemPurchase;
                item.OnUpgrade += OnItemUpgrade;
                item.gameObject.SetActive(true);
            }
        }
    }

    private void OnItemPurchase(string itemName)
    {
        foreach (StoreItemContainer itemContainer in _storeItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemName))
                continue;
            
            itemContainer.Purchase();
            var item = itemContainer.Item;
            var newItem = new ShowcaseItem(item.Name, item.Icon, item.Multiplier, item.MaxLevel, itemContainer.CurrentLevel);
            OnPurchase?.Invoke(newItem, item.PurchaseCost);
            break;
        }
    }

    private void OnItemUpgrade(string itemName, int currentLevel)
    {
        foreach (StoreItemContainer itemContainer in _storeItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemName))
                continue;
            
            itemContainer.Upgrade();
            
            var item = itemContainer.Item;
            var newItem = new ShowcaseItem(item.Name, item.Icon, item.Multiplier, item.MaxLevel, itemContainer.CurrentLevel);
            var upgradeCost = item.GetCurrentUpgradeCost(currentLevel);
            OnUpgrade?.Invoke(newItem, upgradeCost);
            break;
        }
    }
    
    public float GetItemMultiplierBaseByName(string itemName)
    {
        var index = _settings.GetIndexByName(itemName);
        var item = _settings.StoreItems[index];
        return item.Multiplier;
    }

    public int GetItemPurchaseCostByName(string itemName)
    {
        var index = _settings.GetIndexByName(itemName);
        var item = _settings.StoreItems[index];
        return item.PurchaseCost;
    }

    public int GetItemCostByNameAndUpgradeLevel(string itemName, int currentLevel)
    {
        var level = currentLevel == 0 ? currentLevel : currentLevel - 1;
        var index = _settings.GetIndexByName(itemName);
        var item = _settings.StoreItems[index];
        var cost = 0;
        
        for (int i = 0; i < item.UpgradeCost.Count; i++)
        {
            if (i > level)
                break;

            cost += item.UpgradeCost[i];
        }

        return cost;
    }
    
    private void OnDonutsUpdated(int currentDonuts)
    {
        foreach (var itemContainer in _storeItemContainers)
        {
            itemContainer.ChangeActionButtonInteraction(currentDonuts >= itemContainer.CurrentCost);
        }
    }
    
    private void OnShowcaseItemSold(ShowcaseItem itemSold)
    {
        foreach (var itemContainer in _storeItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemSold.Name))
                continue;
            
            var cachedStoreItem = itemContainer.Item;
            itemContainer.Reset();
            itemContainer.SetItem(cachedStoreItem);
            itemContainer.ChangeActionButtonInteraction(GameManager.Instance.CurrentDonuts >= itemContainer.CurrentCost);
        }
    }
}
