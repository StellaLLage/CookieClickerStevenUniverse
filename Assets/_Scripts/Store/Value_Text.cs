using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Value_Text : MonoBehaviour
{   
    
    void Update()
    {

        float f = this.GetComponentInParent<StoreManager>().this_Cost;
        string s = f.ToString("F0");

        switch(transform.parent.GetComponent<StoreManager>().this_Factory){

            case 0:           
                GetComponent<TextMeshProUGUI>().text = "" + s + " Donuts";  
            break;
            case 1:                
                GetComponent<TextMeshProUGUI>().text = "" + s + " Donuts";  
            break;

        }     
    }
}
