using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactoryManager : MonoBehaviour
{
    public StoreManager factories_Values;

    public int this_Factory_M;
    public int this_level;

    public TextMeshProUGUI quantity_Of_Factories;
    public TextMeshProUGUI level_Of_Factory;

   
    void Start()
    {

        GameManager.instance.factoryLevel[this_Factory_M] = this_level;

    }

    void Update()
    {
        switch(factories_Values.this_Factory){

            case 0: 
                quantity_Of_Factories.text = "x" + GameManager.instance.factoryQuantity[0];  
               
            break;
            case 1: 
                quantity_Of_Factories.text = "x" +  GameManager.instance.factoryQuantity[1];  
            break;

        }

        switch(factories_Values.this_Factory){
            case 0: 
                level_Of_Factory.text = "Level " + GameManager.instance.factoryLevel[factories_Values.this_Factory];  
               
            break;
            case 1: 
                level_Of_Factory.text  = "Level" +  GameManager.instance.factoryLevel[factories_Values.this_Factory];  
            break;


        }  
        
    }

}
