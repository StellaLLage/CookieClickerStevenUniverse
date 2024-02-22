using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const string DONUT = "donut";
    private const string DONUTS = "donuts";
    private const string PER_SECOND = "per second:";
    
    [SerializeField] private Button _donutButton;
    [SerializeField] private TextMeshProUGUI _donutCount;
    [SerializeField] private TextMeshProUGUI _multiplierCount;

    public event Action OnDonutClicked;

    private void Awake()
    {
        GameManager.Instance.SetGameController(this);
        _donutButton.onClick.AddListener(OnDonutClick);
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        UpdateDonutCounter(GameManager.Instance.CurrentDonuts);
    }

    private void OnDonutClick()
    {
        OnDonutClicked?.Invoke();
    }

    public void UpdateDonutCounter(int currentDonuts)
    {
        var donuts = currentDonuts == 1 ? DONUT : DONUTS;
        _donutCount.text = $"{currentDonuts} {donuts}";
    }
    
    public void UpdateDonutMultiplier(float currentMultiplier)
    {
        _multiplierCount.text = $"{PER_SECOND} {currentMultiplier:0.00}";
    }
}
