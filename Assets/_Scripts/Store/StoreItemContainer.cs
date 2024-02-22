using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemContainer : ItemContainer
{
    public StoreItem Item { get; private set; }
    
    private bool _canUpgrade;

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space]
    [SerializeField] private TextMeshProUGUI _actionCostText;
    [SerializeField] private Button _actionButton;
    [SerializeField] private TextMeshProUGUI _costValueText;

    public int CurrentLevel => _currentLevel;

    public event Action<string> OnPurchased; 
    public event Action<string, int> OnUpgrade; 
    
    public void SetItem(StoreItem item)
    {
        Item = item;
        
        SetCurrentLevel(0);
        SetIcon(Item.Icon);
        SetName(Item.Name);
        SetDescription(Item.Description);
        SetCostValue(Item.PurchaseCost);
        ChangeCurrentAction(_canUpgrade);
        
        _actionButton.onClick.AddListener(OnClicked);
    }
    
    protected override void SetName(string itemName)
    {
        base.SetName(itemName);
        _nameText.text = itemName;
    }

    private void SetDescription(string itemDescription)
    {
        _descriptionText.text = itemDescription;
    }

    protected override void SetIcon(Sprite icon)
    {
        base.SetIcon(icon);
        _icon.sprite = icon;
    }

    private void SetCostValue(int value)
    {
        _costValueText.text = $"{value} donuts";
    }
    
    private void SetCanUpgrade(bool canUpgrade)
    {
        _canUpgrade = canUpgrade;
    }

    private void ChangeCurrentAction(bool canUpgrade)
    {
        var currentAction = canUpgrade ? "upgrade" : "purchase";
        _actionCostText.text = $"{currentAction} cost:";
    }
    
    protected override void OnClicked()
    {
        base.OnClicked();
        if (_canUpgrade)
        {
            Debug.Log($"OnUpgrade - Current Level {_currentLevel}");
            OnUpgrade?.Invoke(Item.Name, _currentLevel);
        }
        else
        {
            Debug.Log($"OnPurchased {Item.Name}");
            OnPurchased?.Invoke(Item.Name);
        }
    }
    public void Purchase()
    {
        if (_canUpgrade)
        {
            Debug.LogError($"This is shouldn't be true. You can't buy something that is already bought.");
            return; 
        }
        
        SetCanUpgrade(true);
        ChangeCurrentAction(_canUpgrade);
    }

    public void Upgrade()
    {
        if (!_canUpgrade)
        {
            Debug.LogError($"Can't upgrade because CanUpgrade is {_canUpgrade}");
            return; 
        }

        AddLevel();
        SetCurrentLevel(_currentLevel);
        SetCostValue(Item.GetCurrentUpgradeCost(_currentLevel));
    }

    public void ChangeActionButtonInteraction(bool canInteract)
    {
        _actionButton.interactable = canInteract;
    }

    public override void Reset()
    {
        base.Reset();
        SetCurrentLevel(0);
        SetCanUpgrade(false);
        ChangeCurrentAction(_canUpgrade);
        
        _actionButton.onClick.RemoveListener(OnClicked);
    }
}