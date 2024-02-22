using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public StoreController StoreController { get; private set; }
    public GameController GameController { get; private set; }
    public ShowcaseController ShowcaseController { get; private set; }
    
    public int CurrentDonuts => GameController.CurrentDonuts;
    
    public float CurrentMultiplier => GameController.CurrentMultiplier;

    public List<ShowcaseItem> ActiveMultipliers => GameController.ActiveMultipliers;

    public event Action<int> OnDonutsUpdated;
    public event Action<float> OnMultipliersUpdated;
    public event Action<ShowcaseItem> OnShowcaseItemSold;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject); 
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);        
        }
    }

    public void SetShowcaseController(ShowcaseController showcaseController)
    {
        ShowcaseController = showcaseController;
        SubscribeShowcaseControllerEvents();
    }
    
    private void SubscribeShowcaseControllerEvents()
    {
        if (ShowcaseController == null)
        {
            Debug.LogError("Showcase Controller is null. Couldn't subscribe to it events.");
            return;
        }
        
        ShowcaseController.OnSold += GameController.OnItemSold;
    }
    
    private void UnsubscribeShowcaseControllerEvents()
    {
        if (ShowcaseController == null)
        {
            Debug.LogError("Showcase Controller is null. Couldn't unsubscribe to it events.");
            return;
        }
        
        ShowcaseController.OnSold -= GameController.OnItemSold;
    }
    
    public void SetGameController(GameController gameController)
    {
        GameController = gameController;
        SubscribeGameControllerEvents();
    }
    
    private void SubscribeGameControllerEvents()
    {
        if (GameController == null)
        {
            Debug.LogError("Game Controller is null. Couldn't subscribe to it events.");
            return;
        }

        GameController.OnDonutsUpdated += OnDonutsUpdate;
        GameController.OnMultipliersUpdated += OnMultipliersUpdate;
        GameController.OnShowcaseItemSold += OnShowcaseSold;
    }

    private void OnDonutsUpdate(int newDonutsValue)
    {
        OnDonutsUpdated?.Invoke(newDonutsValue);
    }
    
    private void OnMultipliersUpdate(float newMultipliersValue)
    {
        OnMultipliersUpdated?.Invoke(newMultipliersValue);
    }
    
    private void OnShowcaseSold(ShowcaseItem itemSold)
    {
        OnShowcaseItemSold?.Invoke(itemSold);
    }
    
    
    private void UnsubscribeGameControllerEvents()
    {
        if (GameController == null)
        {
            Debug.LogError("Game Controller is null. Couldn't unsubscribe to it events.");
            return;
        }
        
        GameController.OnDonutsUpdated -= OnDonutsUpdate;
        GameController.OnMultipliersUpdated -= OnMultipliersUpdate;
        GameController.OnShowcaseItemSold -= OnShowcaseSold;
    }
    
    public void SetStoreController(StoreController storeController)
    {
        StoreController = storeController;
        SubscribeStoreControllerEvents();
    }

    private void SubscribeStoreControllerEvents()
    {
        if (StoreController == null)
        {
            Debug.LogError("Store Controller is null. Couldn't subscribe to it events.");
            return;
        }
        
        StoreController.OnPurchase += GameController.OnStoreItemUpdated;
        StoreController.OnUpgrade += GameController.OnStoreItemUpdated;
    }
    
    private void UnsubscribeStoreControllerEvents()
    {
        if (StoreController == null)
        {
            Debug.LogError("Store Controller is null. Couldn't unsubscribe to it events.");
            return;
        }
        
        StoreController.OnPurchase -= GameController.OnStoreItemUpdated;
        StoreController.OnUpgrade -= GameController.OnStoreItemUpdated;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}


