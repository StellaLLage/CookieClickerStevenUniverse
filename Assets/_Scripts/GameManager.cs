using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _currentDonuts;
    private Dictionary<string, float> _activeMultipliers = new Dictionary<string, float>();
    private StoreController _storeController;

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

    public void SetStoreController(StoreController storeController)
    {
        _storeController = storeController;
        SubscribeStoreEvents();
    }

    private void SubscribeStoreEvents()
    {
        if (_storeController == null)
        {
            Debug.LogError("Store Controller is null. Couldn't subscribe to it events.");
            return;
        }
        
        _storeController.OnPurchase += OnStoreItemUpdated;
        _storeController.OnUpgrade += OnStoreItemUpdated;
    }
    
    private void UnsubscribeStoreEvents()
    {
        if (_storeController == null)
        {
            Debug.LogError("Store Controller is null. Couldn't unsubscribe to it events.");
            return;
        }
        
        _storeController.OnPurchase -= OnStoreItemUpdated;
        _storeController.OnUpgrade -= OnStoreItemUpdated;
    }
    
    private void OnStoreItemUpdated(string itemName, float multiplierValue)
    {
        TryAddOrUpdateMultipliers(itemName, multiplierValue);
    }
    
    private void TryAddOrUpdateMultipliers(string itemName, float multiplierValue)
    {
        if (_activeMultipliers.ContainsKey(itemName))
            UpdateActiveMultiplier(itemName, multiplierValue);
        else
            CreateNewMultiplier(itemName, multiplierValue);
    }
    
    private void UpdateActiveMultiplier(string itemName, float multiplierValue)
    {
        var upgradedMultiplierValue = multiplierValue;
        upgradedMultiplierValue += _activeMultipliers[itemName];
        _activeMultipliers[itemName] = upgradedMultiplierValue;
        Debug.Log($"Updated active multiplier - Name: {itemName} - New Multiplier {_activeMultipliers[itemName]}");
    }
    
    private void CreateNewMultiplier(string itemName, float multiplierValue)
    {
        _activeMultipliers.Add(itemName, multiplierValue);
        Debug.Log($"Created new multiplier - Name: {itemName} - Starter Multiplier {multiplierValue}");
    }
    
    
    //----------------------------- OLD -------------------------------//
    
    //Factory 
    public int[] factoryIndex;
    public float[] factoryCost;
    public float[] factoryUpgrade;
    public float[] factoryMultiplier;
    public int[] factoryLevel;
    public int[] factoryQuantity;
    public GameObject[] factoryUpgradeButton;
    public static int factories_Sold;

    public int unlocked;


    //Achievements
    public static bool notificationActivated;
    public GameObject notification;
    public string achievement_Name;
    public Sprite achievements_Icon;
    public Transform[] achievements;
    public GameObject achievements_Parent;   
       
    
    //Donuts
    public GameObject clickUpdateButton;
    public float donut; 
    public static int countClick;    
    public float multiplier;    
    public bool[] activateDonutMultiplier;
    public float update_Cost;
    


//---------------------------------------------End Variables -------------------------------------------------------------------------------------// 
    /*
    void Awake()
    {
        if (instance != null){        
            Destroy(gameObject);        
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);        
        }
        
        
        countClick = 0;
        donut = 0;
        multiplier = 1;
        update_Cost = 50;
        
        factoryUpgradeButton = new GameObject[5];
        factoryIndex = new int [5];
        factoryQuantity = new int[5];
        factoryMultiplier = new float[5];
        factoryLevel = new int[5];
        factoryCost = new float[5];
        factoryUpgrade = new float[5];
        factories_Sold = 0;
        
        
        unlocked = 0;
        
        activateDonutMultiplier = new bool[5];
          
        
        for (int i = 0; i < achievements_Parent.transform.childCount; i++){
        
            achievements[i] = achievements_Parent.transform.GetChild(i);
        
        } 
        
        notificationActivated = false;
        
     }
    */

    void Update()
    {/*
        clickUpdateButton.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = update_Cost + " donuts";

        if (donut < update_Cost){

           clickUpdateButton.GetComponent<CanvasGroup>().alpha = 0.5f;
           clickUpdateButton.GetComponent<CanvasGroup>().interactable = false;


        }
        else {

           clickUpdateButton.GetComponent<CanvasGroup>().alpha = 1f;
           clickUpdateButton.GetComponent<CanvasGroup>().interactable = true;            
    
        }
        
        DonutMultiplier();
        Achievements();
        */
    }


    //Clique no donut
    public void Clicker(){
      
        donut += multiplier;
        countClick += 1;

    }

    public void ClickerUpdate(){

        if (donut >= update_Cost){
                                      
            multiplier += 1;

            print("Comprou upgrade");
           
            donut -= update_Cost;
            update_Cost += update_Cost * 2f;     
        }
        else {
          
            print("Não tem dinheiro pra comprar upgrade");

        }


    }

    //Adiciona os multiplicadores de acordo com a fábrica
    public void DonutMultiplier(){

        //Steven
        if(activateDonutMultiplier[0]){

            donut += Time.deltaTime * factoryQuantity[0] * factoryMultiplier[0] * factoryLevel[0];
            
        }

        //Amethyst 
        if (activateDonutMultiplier[1]){

            donut += Time.deltaTime * factoryQuantity[1] * factoryMultiplier[1] * factoryLevel[1];

        }

    }

    #region Achievements
        public void Achievements(){        

            //Se tiver clicado 1 vez
            if (countClick == 1){

                string text = "Well done! You created your first donut.";
                AchievementComplete(0, text);            
                Achievements_Animation(achievement_Name); 
                       
                
            }

            //Se tiver produzido 50 donuts
            if (donut > 49 && donut < 51){

                string text = "Now all your friends and family have donuts.";
                AchievementComplete(1, text);            
                Achievements_Animation(achievement_Name);

            }

            //Se tiver produzido 100 donuts 
            if (donut > 99 && donut < 101){

                string text = "Yay! You should make a party with all these donuts.";
                AchievementComplete(2, text);            
                Achievements_Animation(achievement_Name);  

            }

            //Se tiver produzido 150 donuts
            if (donut > 149 && donut < 151){

                string text = "You really like donuts, heh?";
                AchievementComplete(3, text);            
                Achievements_Animation(achievement_Name); 

            }

            //Se tiver produzido 1000 donuts 
            if (donut > 999 && donut < 1001){

                string text = "Wow! You are really invested! Now Lars and Saddie can take a vacation.";
                AchievementComplete(4, text);            
                Achievements_Animation(achievement_Name);

            }

            //Se tiver comprado seu primeiro Steven (Fábrica)
            if (factoryQuantity[0] == 1){

                string text = "I'm glad you met Steven! You both love donuts.";
                AchievementComplete(5, text);            
                Achievements_Animation(achievement_Name);  

            }

            //Se tiver comprado sua primeira Amethyst (Fábrica)
            if (factoryQuantity[1] == 1){

                string text = "Amethyst is a great friend, just like you!";
                AchievementComplete(6, text);            
                Achievements_Animation(achievement_Name); 

            }

            //Se tiver 5 Stevens ou Amethysts
            if (factoryQuantity[0] == 5 || factoryQuantity[1] == 5){

                string text = "With all these friends you can make a big party.";
                AchievementComplete(7, text);            
                Achievements_Animation(achievement_Name); 

            }

            //Se tiver feito um upgrade em alguma fábrica
            if (factoryLevel[0] == 2 || factoryLevel[1] == 2){

                string text = "You are taking this very serious. Now you are going to have a lot of donuts to eat.";
                AchievementComplete(8, text);            
                Achievements_Animation(achievement_Name); 

            }

            //Se tiver level 5 de upgrade em alguma fábrica
            if (factoryLevel[0] == 5 || factoryLevel[1] == 5){

                string text = "You and your friends are going to make a lot of donuts!";
                AchievementComplete(9, text);            
                Achievements_Animation(achievement_Name);
                

            }


        }        

        public void Achievements_Animation(string name){

            notification.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = name;

            if(notificationActivated != true){

                notification.GetComponent<Animator>().SetBool("Notification", true);
                StartCoroutine(WaitToEndNotification());     
                          
            }
            else {

                StartCoroutine(WaitToStartNotification());
                StartCoroutine(WaitToEndNotification());                

            } 

        }

        IEnumerator WaitToStartNotification(){
            
            yield return new WaitForSeconds(0.5f);
            notification.GetComponent<Animator>().SetBool("Notification", true);

        }

        IEnumerator WaitToEndNotification(){


            yield return new WaitForSeconds(1f);
            notification.GetComponent<Animator>().SetBool("Notification", false);
            notificationActivated = false;

        }

        public void AchievementComplete(int numberOfGameObject, string textAchievementComplete){

            achievements[numberOfGameObject].GetChild(2).GetComponentInChildren<Image>().sprite = achievements_Icon;
            achievements[numberOfGameObject].GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = textAchievementComplete;
            achievement_Name = achievements[numberOfGameObject].GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text;
         
        }

    #endregion  

    public void Sell(int factory_number){

        if(factoryQuantity[factory_number] > 0){
                       
            factories_Sold += 1;
            factoryQuantity[factory_number] -= 1;

        }
        else{

            print("Não pode vender.");

        }


    }

    public void Upgrade_Button(int factory_number){
/*
        float store_Multiplier;
        if(factoryIndex[factory_number] == 0){            
            
            //Multiplier aumenta a metade do valor que tinha
            store_Multiplier = factoryMultiplier[factory_number];            
            store_Multiplier += store_Multiplier * 0.5f;
            factoryMultiplier[factory_number] = store_Multiplier;           
            
            //Desconta o valor nos donuts
            GameManager.instance.donut -= factoryUpgrade[factory_number];
            //Aumenta a quantidade do level 
            GameManager.instance.factoryLevel[factory_number] += 1;
            //Aumenta o preço do upgrade depois de usado
            float store_upgrade = factoryUpgrade[factory_number];
            store_upgrade = store_upgrade * 2f;
            factoryUpgrade[factory_number] = store_upgrade;

        }
        if(factoryIndex[factory_number] == 1){            
            
            //Multiplier aumenta a metade do valor que tinha
            store_Multiplier = factoryMultiplier[factory_number];            
            store_Multiplier += store_Multiplier * 0.5f;
            factoryMultiplier[factory_number] = store_Multiplier;           
            
            //Desconta o valor nos donuts
            GameManager.instance.donut -= factoryUpgrade[factory_number];
            //Aumenta a quantidade do level 
            GameManager.instance.factoryLevel[factory_number] += 1;
            //Aumenta o preço do upgrade depois de usado
            float store_upgrade = factoryUpgrade[factory_number];
            store_upgrade = store_upgrade * 2f;
            factoryUpgrade[factory_number] = store_upgrade;

        }*/

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
