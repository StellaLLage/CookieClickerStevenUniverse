using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DonutUIContainer : MonoBehaviour
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
        _donutButton.onClick.AddListener(OnDonutClick);
        GameManager.Instance.OnDonutsUpdated += UpdateDonutCounter;
        GameManager.Instance.OnMultipliersUpdated += UpdateDonutMultiplier;
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

    private void UpdateDonutCounter(int currentDonuts)
    {
        var donuts = currentDonuts <= 1 ? DONUT : DONUTS;
        _donutCount.text = $"{currentDonuts} {donuts}";
    }
    
    private void UpdateDonutMultiplier(float currentMultiplier)
    {
        _multiplierCount.text = $"{PER_SECOND} {currentMultiplier:0.0}";
    }
}
