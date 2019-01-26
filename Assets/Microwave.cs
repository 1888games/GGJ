using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : AbstractTarget
{
    
    public override void React()
    {
        if (ToddlerController.CurrentTool == null)
        {
            // do stuff
        }
        else
        {
            // explode.
        }
    }
}
