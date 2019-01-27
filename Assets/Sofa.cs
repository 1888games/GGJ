using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sofa : AbstractTarget
{
    public override void React()
    {
        if (IsReactableTool())
        {
            Deface();
        }
        else
        {
            // do nothing
        }
    }

    private void Deface()
    {
        // TODO: change sofa material
    }
}
