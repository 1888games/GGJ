using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : AbstractTarget
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override bool TryReact()
    {
        audio.PlayOneShot(Resources.Load<AudioClip>("Steve/Boing"));

        return true;
    }
}
