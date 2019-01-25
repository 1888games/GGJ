using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(ToddlerController toddlerController);

    protected void changeDirection(ToddlerController toddlerController, Vector3 direction)
    {
        if (!toddlerController.CurrentDirections.Contains(direction))
            toddlerController.CurrentDirections.Add(direction);
    }
}
