using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseItemContainer : ItemContainer
{
    public ShowcaseItem Item { get; private set; }

    private const string LEVEL = "Level";
    
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _currentMultiplierText;
    [SerializeField] private Button _actionButton;

    public event Action<ShowcaseItem> OnSold;
    
    public void SetItem(ShowcaseItem item)
    {
        Item = item;
        SetCurrentLevel(0);
        SetIcon(Item.Icon);
        SetName(Item.Name);
        SetLevel(Item.CurrentLevel);
        SetMultiplier(Item.Multiplier);
        
        _actionButton.onClick.AddListener(OnClicked);
    }
    
    protected override void SetIcon(Sprite icon)
    {
        base.SetIcon(icon);
        _icon.sprite = icon;
    }
    
    protected override void SetName(string itemName)
    {
        base.SetName(itemName);
        _nameText.text = itemName;
    }
    
    private void SetLevel(int level)
    {
        _currentLevelText.text = $"{LEVEL} {level + 1}";
    }
    
    private void SetMultiplier(float multiplier)
    {
        _currentMultiplierText.text = $"x{multiplier:0.0}";
    }
    
    protected override void OnClicked()
    {
        base.OnClicked();
        OnSold?.Invoke(Item);
    }

    public void Sell()
    {
        Reset();
        gameObject.SetActive(false);
    }
    
    public override void Reset()
    {
        base.Reset();
        SetName("??");
        SetCurrentLevel(0);
        SetLevel(0);
        SetMultiplier(0);
        _actionButton.onClick.RemoveListener(OnClicked);
    }
    
}
