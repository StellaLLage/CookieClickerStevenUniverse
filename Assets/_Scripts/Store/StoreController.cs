using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    [SerializeField] private StoreSettings _settings;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private StoreItemContainer _itemContainerRef;
    private List<StoreItemContainer> _storeItemContainers = new List<StoreItemContainer>();

    public event Action<ShowcaseItem> OnPurchase;
    public event Action<ShowcaseItem> OnUpgrade;

    private void Awake()
    {
        GameManager.Instance.SetStoreController(this);
        SetupStore();
    }

    private void SetupStore()
    {
        var storeItems = _settings.StoreItems;
        var missingContainers = storeItems.Count - _storeItemContainers.Count;
        CreateMissingContainers(missingContainers);
        SetupItemContainers(storeItems);
    }

    private void CreateMissingContainers(int missingContainersValue)
    {
        for (int i = 0; i < missingContainersValue; i++)
        {
            var storeItem = Instantiate(_itemContainerRef, _itemsParent);
            _storeItemContainers.Add(storeItem);
        }
    }

    private void SetupItemContainers(List<StoreItem> storeItems)
    {
        for (int i = 0; i < _storeItemContainers.Count; i++)
        {
            var item = _storeItemContainers[i];
            
            if (i >= storeItems.Count)
                item.gameObject.SetActive(false);
            else
            {
                item.SetItem(storeItems[i]);
                item.OnPurchased += OnItemPurchase;
                item.OnUpgrade += OnItemUpgrade;
                item.gameObject.SetActive(true);
            }
        }
    }

    private void OnItemPurchase(string itemName)
    {
        foreach (StoreItemContainer itemContainer in _storeItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemName))
                continue;
            
            itemContainer.Purchase();
            var item = itemContainer.Item;
            var newItem = new ShowcaseItem(item.Name, item.Icon, item.Multiplier, item.MaxLevel, itemContainer.CurrentLevel);
            OnPurchase?.Invoke(newItem);
            break;
        }
    }

    private void OnItemUpgrade(string itemName, int currentLevel)
    {
        foreach (StoreItemContainer itemContainer in _storeItemContainers)
        {
            if (!string.Equals(itemContainer.Item.Name, itemName))
                continue;
            
            itemContainer.Upgrade();
            
            var item = itemContainer.Item;
            var newItem = new ShowcaseItem(item.Name, item.Icon, item.Multiplier, item.MaxLevel, itemContainer.CurrentLevel);
            OnUpgrade?.Invoke(newItem);
            break;
        }
    }
    
    
    public static StoreManager instance;


    public GameObject factory_GameObjectInTheStore;

    public int this_Factory;
    public float this_Cost;
    public float this_Upgrade;
    public float this_Multiplier;

   
    public GameObject this_UpgradeButton;
    

    void Start()
    {
       /* GameManager.instance.factoryIndex[this_Factory] = this_Factory;
        GameManager.instance.factoryCost[this_Factory] = this_Cost;
        GameManager.instance.factoryMultiplier[this_Factory] = this_Multiplier;
        GameManager.instance.factoryUpgrade[this_Factory] = this_Upgrade;
        GameManager.instance.factoryUpgradeButton[this_Factory] = this_UpgradeButton;     
        GameManager.instance.factoryUpgradeButton[this_Factory].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.factoryUpgrade[this_Factory] + " donuts";
    */
    }
        
    void Update()
    {
        /*
        GameManager.instance.factoryUpgradeButton[this_Factory].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.factoryUpgrade[this_Factory] + " donuts";

        if (GameManager.instance.donut < GameManager.instance.factoryCost[this_Factory]){

            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().alpha = 0.5f;

        }
        else {

            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().alpha = 1f;

        }



        if (GameManager.instance.donut < GameManager.instance.factoryUpgrade[this_Factory]){

            GameManager.instance.factoryUpgradeButton[this_Factory].GetComponent<Button>().interactable = false;

        }
        else {

            GameManager.instance.factoryUpgradeButton[this_Factory].GetComponent<Button>().interactable = true;
            '
    
        }*/
        
    }

    public void BuyFactory(){
       /*
        //Confere se o jogador tem donuts disponíveis e se a quantidade é maior ou igual ao preço da fábrica para poder comprar a fábrica
        if (GameManager.instance.donut >= GameManager.instance.factoryCost[this_Factory]){

            if(GameManager.instance.factoryQuantity[this_Factory] <= 0){

                factory_GameObjectInTheStore.SetActive(true);                
                GameManager.instance.factoryQuantity[this_Factory] += 1;                
                GameManager.instance.activateDonutMultiplier[this_Factory] = true;

            } else {              

                GameManager.instance.factoryQuantity[this_Factory] += 1;


            }

            print("Comprou " + this_Factory);
           
            GameManager.instance.donut -= GameManager.instance.factoryCost[this_Factory];
            this_Cost += this_Cost * 1.5f;
            GameManager.instance.factoryCost[this_Factory] = this_Cost;

     
            
        }
        else {
          
            print("Não tem dinheiro");

        }
*/
    }
}
