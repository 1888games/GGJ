using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachine : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;

    public override void React()
    {
        if (IsReactableTool())
        {
            Wash();
        }
        else
        {
            // do nothing
        }
    }

    private void Wash()
    {
        print("Washing machine success reaction.");

        ToddlerController.Instance.DestroyTool();

        // TODO: Need correct sound for washing machine 

        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();
        Fabric.EventManager.Instance.PostEvent("Washing_Machine_Sequence", Fabric.EventAction.PlaySound, null, gameObject);

        ToddlerController.Instance.OnMadeNoise();
    }
}
