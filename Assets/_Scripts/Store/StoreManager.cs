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
        GameManager.Instance.factoryIndex[this_Factory] = this_Factory;
        GameManager.Instance.factoryCost[this_Factory] = this_Cost;
        GameManager.Instance.factoryMultiplier[this_Factory] = this_Multiplier;
        GameManager.Instance.factoryUpgrade[this_Factory] = this_Upgrade;
        GameManager.Instance.factoryUpgradeButton[this_Factory] = this_UpgradeButton;     
        GameManager.Instance.factoryUpgradeButton[this_Factory].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.factoryUpgrade[this_Factory] + " donuts";
    }
        
    void Update()
    {
        GameManager.Instance.factoryUpgradeButton[this_Factory].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.factoryUpgrade[this_Factory] + " donuts";

        if (GameManager.Instance.donut < GameManager.Instance.factoryCost[this_Factory]){

            GetComponent<CanvasGroup>().interactable = false;
            GetComponent<CanvasGroup>().alpha = 0.5f;

        }
        else {

            GetComponent<CanvasGroup>().interactable = true;
            GetComponent<CanvasGroup>().alpha = 1f;

        }



        if (GameManager.Instance.donut < GameManager.Instance.factoryUpgrade[this_Factory]){

            GameManager.Instance.factoryUpgradeButton[this_Factory].GetComponent<Button>().interactable = false;

        }
        else {

            GameManager.Instance.factoryUpgradeButton[this_Factory].GetComponent<Button>().interactable = true;
            
    
        }
        
    }

    public void BuyFactory(){
       
        //Confere se o jogador tem donuts disponíveis e se a quantidade é maior ou igual ao preço da fábrica para poder comprar a fábrica
        if (GameManager.Instance.donut >= GameManager.Instance.factoryCost[this_Factory]){

            if(GameManager.Instance.factoryQuantity[this_Factory] <= 0){

                factory_GameObjectInTheStore.SetActive(true);                
                GameManager.Instance.factoryQuantity[this_Factory] += 1;                
                GameManager.Instance.activateDonutMultiplier[this_Factory] = true;

            } else {              

                GameManager.Instance.factoryQuantity[this_Factory] += 1;


            }

            print("Comprou " + this_Factory);
           
            GameManager.Instance.donut -= GameManager.Instance.factoryCost[this_Factory];
            this_Cost += this_Cost * 1.5f;
            GameManager.Instance.factoryCost[this_Factory] = this_Cost;

     
            
        }
        else {
          
            print("Não tem dinheiro");

        }

    }
}
