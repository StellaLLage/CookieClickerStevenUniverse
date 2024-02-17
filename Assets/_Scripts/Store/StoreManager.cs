using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{   
    public static StoreManager instance;


    public GameObject factory_GameObjectInTheStore;

    public int this_Factory;
    public float this_Cost;
    public float this_Upgrade;
    public float this_Multiplier;

   
    public GameObject this_UpgradeButton;
    

    void Start()
    {
        GameManager.instance.factoryIndex[this_Factory] = this_Factory;
        GameManager.instance.factoryCost[this_Factory] = this_Cost;
        GameManager.instance.factoryMultiplier[this_Factory] = this_Multiplier;
        GameManager.instance.factoryUpgrade[this_Factory] = this_Upgrade;
        GameManager.instance.factoryUpgradeButton[this_Factory] = this_UpgradeButton;     
        GameManager.instance.factoryUpgradeButton[this_Factory].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.instance.factoryUpgrade[this_Factory] + " donuts";
    }
        
    void Update()
    {
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
            
    
        }
        
    }

    public void BuyFactory(){
       
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

    }
   
    
}
