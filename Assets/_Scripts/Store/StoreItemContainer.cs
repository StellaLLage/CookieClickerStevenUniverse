using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemContainer : MonoBehaviour
{
    private int _currentLevel; 
    private bool _canUpgrade; 
    
    [SerializeField] private Image _iconSprite;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space]
    [SerializeField] private TextMeshProUGUI _actionCostText;
    [SerializeField] private Button _actionButton;
    [SerializeField] private TextMeshProUGUI _costValueText;

    public StoreItem Item { get; private set; }

    public event Action<string> OnPurchased; 
    public event Action<string, int> OnUpgrade; 
    
    public void SetStoreItem(StoreItem item)
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
    
    private void AddLevel()
    {
        _currentLevel += 1;
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
}
