using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTarget : MonoBehaviour
{
<<<<<<< HEAD
    protected bool waitingForConfirmation;
=======
    protected AudioSource audio;
    [SerializeField] protected List<AbstractTool> _itemsIWillReactWith; 

    protected bool waitingForConfirmation;
>>>>>>> 1ba445088ead54f4bfd23afd2c2680fe48bfb71e

    // Start is called before the first frame update
    public virtual void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tool"))
        {
=======
        if (other.gameObject.CompareTag("Player"))
        {
>>>>>>> 1ba445088ead54f4bfd23afd2c2680fe48bfb71e
            waitingForConfirmation = true;
            InteractionAttempt(ToddlerController.CurrentTool, this);
        }
    }

    public abstract bool TryReact();

    public static Action<string> ReactionComplete;

    public static Action<AbstractTool, AbstractTarget> ConfirmationReceived;

    public static Action<AbstractTool, AbstractTarget> InteractionAttempt;
}
