using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : AbstractTarget
{
    
    public override void React()
    {
        if (ToddlerController.CurrentTool == null)
        {
            Fabric.EventManager.Instance.PostEvent("Simple", Fabric.EventAction.PlaySound, null, gameObject);

            // do stuff
            
        }
        else
        {
            // explode.
        }
    }
}
