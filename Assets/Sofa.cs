using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sofa : AbstractTarget
{
    [SerializeField]
    Material sofaMaterial;

    [SerializeField]
    Texture graffitiedTexture;

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
        sofaMaterial.mainTexture = graffitiedTexture;
    }
}
