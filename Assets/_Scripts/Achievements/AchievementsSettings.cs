using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsSettings", menuName = "DonutClicker/Achievements/AchievementsSettings", order = 1)]
public class AchievementsSettings: ScriptableObject
{
    public List<Achievement> Achievements = new List<Achievement>();

    [Button]
    public void GetAllAchievements()
    {
        Achievements.Clear();
        string[] achievementsFound = AssetDatabase.FindAssets("t:Achievement");

        foreach (string achievementsGuid in achievementsFound)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(achievementsGuid);
            Achievement achievement = (Achievement)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Achievement));
           
            if (!Achievements.Contains(achievement))
                Achievements.Add(achievement);
        }
    }
}
