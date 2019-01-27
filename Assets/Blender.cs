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
        if (IsReactableTool())
        {
            ExperienceController.Instance.UpdateExperienceAndAnguish(name,ToddlerController.CurrentTool.name );
            Blend();
        }
    }

    private void Blend()
    {
        print("Blender success reaction.");

        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();
        Fabric.EventManager.Instance.PostEvent("Broken_Blender", Fabric.EventAction.PlaySound, null, gameObject);

        ToddlerController.Instance.OnMadeNoise();
        ToddlerController.Instance.DestroyTool();
    }
}
