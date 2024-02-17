using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    public void ChangeScene(string scene_name){

        SceneManager.LoadScene(scene_name);

    }

    public void Exit(){

        Application.Quit();

    }
   


}
