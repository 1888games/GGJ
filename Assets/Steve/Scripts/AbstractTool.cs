using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTool : MonoBehaviour
{
    [SerializeField]
    protected AudioClip audioClip;

    protected AudioSource audio;
    protected MeshRenderer meshRenderer;

    protected bool audioPlaying;

    // Start is called before the first frame update
    public virtual void Start()
    {
        audio = GetComponent<AudioSource>();
        if (audioClip != null)
            audio.clip = audioClip;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (IsHeld)
            indicateProximity(false);

        if (IsHeld && audioClip != null && !audioPlaying)
        {
            playAudio(true);
        }
        else if (!IsHeld && audioPlaying)
        {
            playAudio(false);
        }
    }

    /// <summary>
    /// Is this tool held by the player?
    /// </summary>
    /// <value><c>true</c> if is held; otherwise, <c>false</c>.</value>
    public bool IsHeld
    {
        get { return transform.parent != null && transform.parent.CompareTag("Player"); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsHeld)
        {
            indicateProximity(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !IsHeld)
        {
            indicateProximity(false);
        }
    }

    protected virtual void indicateProximity(bool enable = true)
    {
        if (enable)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            meshRenderer.material.color = Color.white;
        }
    }

    protected virtual void playAudio(bool enable = true)
    {
        if (enable)
        {
            audioPlaying = true;
            audio.Play();
        }
        else
        {
            audioPlaying = false;
            audio.Stop();
        }
    }
}
