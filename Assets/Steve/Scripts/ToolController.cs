using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    AudioSource audio;
    MeshRenderer meshRenderer;

    bool audioPlaying;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        if (audioClip != null)
            audio.clip = audioClip;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHeld && audioClip != null && !audioPlaying)
        {
            audioPlaying = true;
            audio.Play();
        }
        else if (!IsHeld && audioPlaying)
        {
            audioPlaying = false;
            audio.Stop();
        }
    }

    public bool IsHeld
    {
        get { return transform.parent != null && transform.parent.CompareTag("Player"); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meshRenderer.material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meshRenderer.material.color = Color.white;
        }
    }
}
