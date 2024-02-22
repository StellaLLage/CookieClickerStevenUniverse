using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseController : MonoBehaviour
{
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShowcaseItemContainer _itemContainerRef;
    private List<ShowcaseItemContainer> _showcaseItemContainers = new List<ShowcaseItemContainer>();

    private void Awake()
    {
        GameManager.Instance.SetShowcaseController(this);
        SetupShowcase();
    }

    private void SetupShowcase()
    {
        var missingContainers =  GameManager.Instance.ActiveMultipliers.Count - _showcaseItemContainers.Count;
        CreateMissingContainers(missingContainers);
       // SetupItemContainers(storeItems);
    }

    private void CreateMissingContainers(int missingContainersValue)
    {
        for (int i = 0; i < missingContainersValue; i++)
        {
            var itemContainer = Instantiate(_itemContainerRef, _itemsParent);
            _showcaseItemContainers.Add(itemContainer);
        }
    }

    private void SetupItemContainers(List<ShowcaseItem> showcaseItems)
    {
        for (int i = 0; i < _showcaseItemContainers.Count; i++)
        {
            var item = _showcaseItemContainers[i];
            /*
            if (i >= storeItems.Count)
                item.gameObject.SetActive(false);
            else
            {
                item.SetItem(storeItems[i]);
                item.OnPurchased += OnItemPurchase;
                item.OnUpgrade += OnItemUpgrade;
                item.gameObject.SetActive(true);
            }*/
        }
    }
/*
    private void OnItemPurchase(string itemName)
    {
        foreach (StoreItemContainer itemContainer in _storeItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemName))
                continue;
            
            itemContainer.Purchase();
            OnPurchase?.Invoke(itemContainer.Item.Name, itemContainer.Item.Multiplier);
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
            OnUpgrade?.Invoke(itemContainer.Item.Name, itemContainer.Item.Multiplier);
            break;
        }
    }
    */
    
}
