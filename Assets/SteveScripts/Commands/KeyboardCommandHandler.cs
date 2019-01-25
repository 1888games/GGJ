using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class KeyboardCommandHandler : CommandHandler
{
    public override IEnumerable<Command> HandleInput()
    {
        List<Command> commands = new List<Command>();

        if (!Input.anyKey)
        {
            commands.Add(doNothing);
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                commands.Add(goLeft);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                commands.Add(goRight);
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                commands.Add(goUp);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                commands.Add(goDown);
            }
        }

        foreach (Command command in commands)
        {
            //commandHistory.Add(Time.time, command);

            yield return command;
        }
    }
}
