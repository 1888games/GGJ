using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    [SerializeField]
    private bool isNoisy;

    AudioSource audio;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
