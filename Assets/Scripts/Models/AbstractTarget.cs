using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractTarget : MonoBehaviour
{
    [SerializeField] protected List<AbstractTool> _itemsIWillReactWith;

    protected bool waitingForConfirmation;

    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    [Button("clear list")]
    void ClearList()
    {
        _itemsIWillReactWith = new List<AbstractTool>();
        _itemsIWillReactWith.Clear();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (waitingForConfirmation && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForConfirmation = false;

            ConfirmationReceived(ToddlerController.CurrentTool, this);

            TryReact();
        }
    }


    [Button("Test reaction")]
    void ForceReaction()
    {
       React(); 
    }

    public virtual bool TryReact()
    {
        if (IsReactableTool())
        {
            print(name + " successful reaction.");
            
            React();
            OnSuccessfulReaction(ToddlerController.CurrentTool, this);
			return true;
        }
        else
        {
			return false;
           // print("interaction fail. current tool: " + ToddlerController.CurrentTool.name);
        }
    }

    public bool IsReactableTool()
    {
        if (ToddlerController.CurrentTool == null)
        {
			//print("Not holding a tool but checking if I can react with it...");
			return false;
        }
        print("looking for ... " + ToddlerController.CurrentTool.name);
        print(_itemsIWillReactWith.Find(tool => tool.name == ToddlerController.CurrentTool.name));
        return _itemsIWillReactWith.Find(tool => tool.name == ToddlerController.CurrentTool.name) != null;
    }

    public abstract void React();

    public static Action<AbstractTool, AbstractTarget> OnSuccessfulReaction = delegate {  };
    
    public static Action<string> ReactionComplete= delegate {  };

    public static Action<AbstractTool, AbstractTarget> ConfirmationReceived = delegate {  };

    public static Action<AbstractTool, AbstractTarget> InteractionAttempt= delegate {  };

    public static Action<AbstractTool, AbstractTarget> OnWalkaway = delegate { };
}
