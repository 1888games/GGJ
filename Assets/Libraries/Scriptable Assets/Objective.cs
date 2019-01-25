using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/List")]
public class Objective : ScriptableObject
{
    public string Description;
    public bool IsComplete{
        get{ return PlayerPrefs.GetInt(name + "complete", 0) == 1; }
        set{PlayerPrefs.SetInt(name + "complete", value ? 1: 0);}
    }
}
