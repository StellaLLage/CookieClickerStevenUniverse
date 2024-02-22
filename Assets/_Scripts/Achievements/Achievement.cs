using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "DonutClicker/Achievements/Achievement", order = 2)]
[Serializable]
public class Achievement : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public string DescriptionUnlocked;
}
