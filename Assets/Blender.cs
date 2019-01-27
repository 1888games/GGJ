using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Blender : AbstractTarget
{    
    
    public override void React()
    {
        print("Blender success reaction.");
        Animation animation = GetComponent<Animation>();
        animation.Play();
        Fabric.EventManager.Instance.PostEvent("Broken_Blender", Fabric.EventAction.PlaySound, null, gameObject);
        print("Toddler instance:" + ToddlerController.Instance);
        ToddlerController.Instance.OnMadeNoise();
    }
}
