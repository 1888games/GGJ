using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : AbstractTarget
{
    public override bool TryReact()
    {
        if (_itemsIWillReactWith.Contains(ToddlerController.CurrentTool))
        {
            // play sound
            // play particle effects
            return true;
        }
        else
        {
            // no reaction.
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
