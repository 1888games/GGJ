using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTool : MonoBehaviour
{
    protected MeshRenderer meshRenderer;

    protected bool audioPlaying;

    // Start is called before the first frame update
    public virtual void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (IsHeld)
            indicateProximity(false);
    }

    /// <summary>
    /// Is this tool held by the player?
    /// </summary>
    /// <value><c>true</c> if is held; otherwise, <c>false</c>.</value>
    public bool IsHeld
    {
        get { return transform.parent != null && transform.parent.CompareTag("Player"); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsHeld)
        {
            indicateProximity(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsHeld)
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
}
