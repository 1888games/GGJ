using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;

    bool isOn;

    public override void React()
    {
        if (IsReactableTool())
        {
            if (isOn)
                TurnOff();
            else
                TurnOn();
        }
        else
        {
            // do nothing
        }
    }

    private void TurnOn()
    {
        isOn = true;

        // TODO: animation and sound effects?

        ToddlerController.Instance.OnMadeNoise();

    }

    private void TurnOff()
    {
        isOn = false;

        // TODO: turn off tv?
    }
}
