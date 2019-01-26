using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTarget : MonoBehaviour
{
    protected AudioSource audio;

    protected bool waitingForConfirmation;

    // Start is called before the first frame update
    public virtual void Start()
    {
        audio = GetComponent<AudioSource>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Fork"))
        {
            audio.PlayOneShot(Resources.Load<AudioClip>("Steve/Boing"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waitingForConfirmation = true;
            InteractionAttempt(ToddlerController.CurrentTool, this);
        }
    }

    public abstract bool TryReact();

    public static Action<string> ReactionComplete;

    public static Action<AbstractTool, AbstractTarget> ConfirmationReceived;

    public static Action<AbstractTool, AbstractTarget> InteractionAttempt;
}
