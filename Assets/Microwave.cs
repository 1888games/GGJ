using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : AbstractTarget
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool TryReact()
    {
        if (_itemsIWillReactWith.Contains(ToddlerController.CurrentTool))
        {
            // blow up;
            return true;
        }
        else
        {
            // no reaction.
            return false;
        }
    }
}
