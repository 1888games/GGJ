

public class Chair : AbstractTarget
{
    public override bool TryReact()
    {
        if (_itemsIWillReactWith.Contains(ToddlerController.CurrentTool))
        {
            // react.
            return true;
        }
        else
        {
            // no reaction.
            return false;
        }
        
    }
}
