using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gem_Text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float f = GameManager.Instance.donut;
        string s = f.ToString("F0");
        GetComponent<TextMeshProUGUI>().text = "" + s + " Donuts";
        
    }
}
