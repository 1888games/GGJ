using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDownCommand : Command
{
    public override void Execute(ToddlerController toddlerController)
    {
        changeDirection(toddlerController, Direction.South);
    }
}
