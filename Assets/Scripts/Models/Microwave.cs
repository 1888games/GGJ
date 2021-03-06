﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : AbstractTarget
{
    [SerializeField] GameObject _noisePrefab;
    [SerializeField] ParticleSystem _firePS;

    public override void React()
    {
        if (ToddlerController.CurrentTool is IMetallic)
        {
            killMicrowave();
        }
        else
        {
            runMicrowave();
        }
    }

    private void killMicrowave()
    {
        print("Microwave kill success reaction.");


        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();

        Fabric.EventManager.Instance.PostEvent("Microwave_Sequence", Fabric.EventAction.PlaySound, null, gameObject);
        ExperienceController.Instance.UpdateExperienceAndAnguish(name, ToddlerController.CurrentTool.name);
//        Fabric.EventManager.Instance.PostEvent("Microwave_Close", Fabric.EventAction.PlaySound, null, gameObject);
//        Fabric.EventManager.Instance.PostEvent("Microwave_Beep", Fabric.EventAction.PlaySound, null, gameObject);
//        Fabric.EventManager.Instance.PostEvent("Microwave_On", Fabric.EventAction.PlaySound, null, gameObject);
//        Fabric.EventManager.Instance.PostEvent("Microwave_Electric_Shock", Fabric.EventAction.PlaySound, null, gameObject);
//        Fabric.EventManager.Instance.PostEvent("Microwave_Fail_Beep", Fabric.EventAction.PlaySound, null, gameObject);
        _firePS.Play();
        ToddlerController.Instance.OnMadeNoise();
        ToddlerController.Instance.DestroyTool();

    }

    private void runMicrowave()
    {
        print("Microwave run success reaction.");

        Fabric.EventManager.Instance.PostEvent("Microwave_Open", Fabric.EventAction.PlaySound, null, gameObject);
        Fabric.EventManager.Instance.PostEvent("Microwave_Close", Fabric.EventAction.PlaySound, null, gameObject);
        Fabric.EventManager.Instance.PostEvent("Microwave_Beep", Fabric.EventAction.PlaySound, null, gameObject);
        Fabric.EventManager.Instance.PostEvent("Microwave_On", Fabric.EventAction.PlaySound, null, gameObject);

        ToddlerController.Instance.OnMadeNoise();
    }
}
