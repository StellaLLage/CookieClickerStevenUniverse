using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsContainer : MonoBehaviour
{
    private Achievement _achievement;
    
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _icon;

    public void SetAchievement(Achievement achievement)
    {
        _achievement = achievement;
        SetTitle(_achievement.Name);
        SetDescription(_achievement.Description);
    }
    
    private void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }
    private void SetTitle(string title)
    {
        _titleText.text = title;
    }
    
    private void SetDescription(string description)
    {
        _descriptionText.text = description;
    }
}
