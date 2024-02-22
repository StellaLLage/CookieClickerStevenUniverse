using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemContainer : MonoBehaviour
{
    protected int _currentLevel { get; private set; }
    protected virtual void SetName(string itemName) { }
    protected virtual void SetIcon(Sprite icon) {} 
    protected void SetCurrentLevel(int currentLevel) => _currentLevel = currentLevel;
    protected void AddLevel() => _currentLevel += 1;
    
    protected virtual void OnClicked() { }
    
    public virtual void Reset() => SetCurrentLevel(0);
}