using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/List")]
public class ObjectivesList : ScriptableObject
{
   public List<Objective> List;
   public int GetCurrentObjectiveIndex()
   {
      return List.IndexOf(GetCurrentObjective());
   }
   public Objective GetCurrentObjective()
   {
      for (int i = 0; i < List.Count; i++)
      {
         if (List[i].IsComplete == false) return List[i];
      }
      return List[List.Count - 1];
   }

   public bool HasCompletedGame()
   {
      return List[List.Count -1].IsComplete;
   }

   [Button("Reset progress")]
   void ResetProgress()
   {
      foreach (var objective in List)
      {
         objective.IsComplete = false;
         Objective.OnObjectiveComplete();
      }
   }
}
