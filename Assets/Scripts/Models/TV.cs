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
        isOn = TVController.Instance.ToggleTV();

        ToddlerController.Instance.OnMadeNoise();

    }

    private void TurnOff()
    {
        isOn = TVController.Instance.ToggleTV();
    }
}
