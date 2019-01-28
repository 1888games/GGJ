using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : AbstractTarget
{
    AudioSource audio;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        audio = this.gameObject.AddComponent<AudioSource>();
    }
    public override void React()
    {
        audio.PlayOneShot(Resources.Load<AudioClip>("Steve/Boing"));
    }
}
