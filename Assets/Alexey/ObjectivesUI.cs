using TMPro;
using UnityEngine;

public class ObjectivesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _objectiveText;
    [SerializeField] ObjectivesList _objectivesList;
    void Awake()
    {
        Objective.OnObjectiveComplete += UpdateObjectiveUI;
        UpdateObjectiveUI();
    }

    void UpdateObjectiveUI()
    {
        if (_objectivesList.HasCompletedGame() == false)
        {
            _objectiveText.text = string.Format("{0}:{1}", 
                _objectivesList.GetCurrentObjectiveIndex() + 1,
                _objectivesList.GetCurrentObjective().Description);    
        }
        else
        {
            _objectiveText.text = "Game over";
        }
        
    }
}
