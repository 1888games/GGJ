using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Blender : AbstractTarget
{
    void OnEnable()
    {
        ToddlerController.OnChaseModeFinished += ResetState;
    }

    void ResetState()
    {
        Animation animation = GetComponent<Animation>();
        animation.Stop();
        transform.rotation = Quaternion.identity;
    }
    
    public override void React()
    {
        ToddlerController.Instance.DestroyTool();
        Animation animation = GetComponent<Animation>();
        animation.Play();
        Fabric.EventManager.Instance.PostEvent("Broken_Blender", Fabric.EventAction.PlaySound, null, gameObject);
        ToddlerController.Instance.OnMadeNoise();
    }
}
