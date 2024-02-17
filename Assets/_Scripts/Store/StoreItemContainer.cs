using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemContainer : MonoBehaviour
{
    private StoreItem _item;
    private int _currentLevel; 
    private bool _canUpgrade; 
    
    [SerializeField] private Image _iconSprite;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space]
    [SerializeField] private TextMeshProUGUI _actionCostText;
    [SerializeField] private Button _actionButton;
    [SerializeField] private TextMeshProUGUI _costValueText;

    public event Action OnPurchased; 
    public event Action<int> OnUpgrade; 
    
    public void SetStoreItem(StoreItem item)
    {
        _item = item;

        SetCurrentLevel(0);
        SetIcon(_item.Icon);
        SetName(_item.Name);
        SetDescription(_item.Description);
        SetCostValue(_item.PurchaseCost);
        ChangeCurrentAction(_canUpgrade);
        
        _actionButton.onClick.AddListener(OnClicked);
    }

    public void UpgradeStoreItem()
    {
        if (!_canUpgrade)
        {
            SetCanUpgrade(true);
            ChangeCurrentAction(_canUpgrade);
        }
        
        SetCurrentLevel(_currentLevel++);
        SetCostValue(_item.GetCurrentUpgradeCost(_currentLevel));
    }
    
    public void Reset()
    {
        SetCurrentLevel(0);
        SetCanUpgrade(false);
        ChangeCurrentAction(_canUpgrade);
        _actionButton.onClick.RemoveListener(OnClicked);
    }


    private void SetCanUpgrade(bool canUpgrade)
    {
        _canUpgrade = canUpgrade;
    }

    private void SetCurrentLevel(int level)
    {
        _currentLevel = level;
    }

    private void SetIcon(Sprite icon)
    {
        _iconSprite.sprite = icon;
    }
    private void SetName(string itemName)
    {
        _nameText.text = itemName;
    }
    
    private void SetDescription(string itemDescription)
    {
        _descriptionText.text = itemDescription;
    }
    
    private void SetCostValue(int value)
    {
        _costValueText.text = $"{value} donuts";
    }

    private void ChangeCurrentAction(bool canUpgrade)
    {
        var currentAction = canUpgrade ? "upgrade" : "purchase";
        _actionCostText.text = $"{currentAction} cost:";
    }
    
    private void OnClicked()
    {
        if (_canUpgrade)
            OnUpgrade?.Invoke(_currentLevel);
        else
            OnPurchased?.Invoke();
    }
}
