using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
    public override void React()
    {
        if (IsReactableTool())
        {
            TurnOn();
        }
        else
        {
            // do nothing
        }
    }

    private void TurnOn()
    {
        // TODO: animation and sound effects?

        ToddlerController.Instance.OnMadeNoise();
    }
}
