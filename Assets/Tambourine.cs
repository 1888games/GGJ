
public class Tambourine : AbstractTarget
{
    
    public override bool TryReact()
    {
        if (_itemsIWillReactWith.Contains(ToddlerController.CurrentTool))
        {
            print("play tambourine sound");
            return true;
        }
        else
        {
            print("interaction fail.");
            return false;
        }
    }
}
