using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbstractTool : MonoBehaviour
{
    protected MeshRenderer meshRenderer;

    protected bool audioPlaying;
	bool inRange = false;
	bool highlighted = false;

	Color originalColor;

    // Start is called before the first frame update
    public virtual void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

		
    }

    // Update is called once per frame
    public virtual void Update()
    {

		if (IsHeld) {
			indicateProximity (false);
			return;
		}

	

		if (inRange) {
			if (ToddlerController.Instance.isExploring ()) {
				if (highlighted == false) {
					indicateProximity (true);
				}
			} else {
				if (highlighted) {
					indicateProximity (false);
				}
			}
		} else {
			
			if (highlighted) {
					indicateProximity (false);
			}

		}
    }

    /// <summary>
    /// Is this tool held by the player?
    /// </summary>
    /// <value><c>true</c> if is held; otherwise, <c>false</c>.</value>
    public bool IsHeld
    {
        get { return transform.parent != null && transform.parent.parent != null && transform.parent.parent.CompareTag("Player"); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsHeld)
        {
			inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsHeld)
        {
			inRange = false;
			indicateProximity (false);
        }
    }


	void SetMeshColor (MeshRenderer mesh, bool enable = true) {
		
		if (mesh != null) {
			if (enable) {
				//Debug.Log ("ORIG1: " + originalColor);
				originalColor = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b);
				
				if (mesh.transform.name == "Vernon") {
					Debug.Log ("BACK: " + originalColor + " " + inRange + " " + highlighted + " " + enable );
				}
				mesh.material.color = Color.red;
			} else {

				if (originalColor.r > 0f || originalColor.g > 0f) {
					mesh.material.color = originalColor;
						if (mesh.transform.name == "Vernon") {
							Debug.Log ("BACK: " + originalColor + " " + inRange + " " + highlighted + " " + enable );
						}
				}
				
				
			}

		}

	}

    protected virtual void indicateProximity(bool enable = true)
    {

		highlighted = enable;
		//SetMeshColor (meshRenderer, enable);

		foreach (Transform child in this.transform.Find("Trigger")) {

				SetMeshColor (child.GetComponent<MeshRenderer> (), enable);
		}
		
	
    }
}
