using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsController : MonoBehaviour
{
    
    
    
    /*
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

    #endregion  */
}
