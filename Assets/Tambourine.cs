
using UnityEngine;

public class Tambourine : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
    public override void React()
    {
        if (IsReactableTool())
        {
            ExperienceController.Instance.UpdateExperienceAndAnguish(name,ToddlerController.CurrentTool.name );
            Rattle();
        }
    }

    private void Rattle()
    {
        print("Tambourine success reaction.");

        Fabric.EventManager.Instance.PostEvent("Toy_Tambourine", Fabric.EventAction.PlaySound, null, gameObject);

        ToddlerController.Instance.OnMadeNoise();
    }
}
