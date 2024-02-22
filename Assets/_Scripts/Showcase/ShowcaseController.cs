using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseController : MonoBehaviour
{
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShowcaseItemContainer _itemContainerRef;
    private List<ShowcaseItemContainer> _showcaseItemContainers = new List<ShowcaseItemContainer>();

    public event Action<ShowcaseItem> OnSold;
    
    private void Awake()
    {
        GameManager.Instance.SetShowcaseController(this);
        GameManager.Instance.OnMultipliersUpdated += OnMultipliersUpdated;
        SetupShowcase();
    }

    private void SetupShowcase()
    {
        var activeMultipliers = GameManager.Instance.ActiveMultipliers;
        var missingContainers =  activeMultipliers.Count - _showcaseItemContainers.Count;
        CreateMissingContainers(missingContainers);
        SetupItemContainers(activeMultipliers);
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
            
            if (i >= showcaseItems.Count)
                item.gameObject.SetActive(false);
            else
            {
                item.SetItem(showcaseItems[i]);
                item.OnSold += OnItemSold;
                item.gameObject.SetActive(true);
            }
        }
    }

    private void OnItemSold(ShowcaseItem itemSold)
    {
        foreach (ShowcaseItemContainer itemContainer in _showcaseItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemSold.Name))
                continue;
            
            itemContainer.Sell();
            OnSold?.Invoke(itemSold);
            break;
        }
    }
    
    private void OnMultipliersUpdated(float newMultipliers)
    {
        Debug.Log("Passou aqui");
        SetupShowcase();
    }
}
