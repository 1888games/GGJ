using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/New")]
public class Objective : ScriptableObject
{
    public static Action OnObjectiveComplete = delegate {};
    public string Description;
    public bool IsComplete{
        get{ return PlayerPrefs.GetInt(name + "complete", 0) == 1; }
        set{
            PlayerPrefs.SetInt(name + "complete", value ? 1: 0);
            if (value) OnObjectiveComplete();
        }
    }

    [Button("Complete")]
    void ForceCompleteObjective()
    {
        IsComplete = true;
    }
}
