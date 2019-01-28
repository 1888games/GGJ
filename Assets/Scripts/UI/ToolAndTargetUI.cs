
using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
 
public class ToolAndTargetUI : MonoBehaviour {
 
    public CanvasGroup carryingBar;
    public CanvasGroup targetBar;
    public CanvasGroup plusBar;
 
    public TextMeshProUGUI carryingText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI spaceText;

	public GameObject optionPrefab;
	public GameObject targetOptionPrefab;

	bool redraw = true;
	bool redrawTargets = true;
 
    void Awake () {
		// listen to tools and targets state changes

		//

		ToddlerController.OnPickedUp += OnPickedUpObject;
		ToddlerController.OnDropped += OnDroppedObject;
		ToddlerController.PickupAttempt += OnObjectInRange;
		ToddlerController.OnWalkaway += OnCancelPickup;
		

		AbstractTarget.InteractionAttempt += OnApproachObject;
		AbstractTarget.OnWalkaway += OnCancelInteract;

		AbstractTarget.OnSuccessfulReaction += OnCancelInteract;
		
		
    }
 
    void UpdateUI () {
 
    }
 
    [Button ("test pickup")]
    void TestPickup () {
 
        OnPickedUpObject (null);
 
    }
 
    [Button ("test drop")]
    void TestDrop () {
 
        OnDroppedObject (null);
 
    }
 
    [Button ("test approach")]
    void TestApproach () {
 
        OnApproachObject (null, null);
 
    }
     
    [Button ("test attemptPickup")]
    void TestAttemptPickup () {
 
        OnObjectInRange (null);
 
    }


	void RemoveOptions () {

		foreach (Transform child in carryingBar.transform) {

			Destroy (child.gameObject);

		}

	}


	void RemoveTargetOptions () {
	
		foreach (Transform child in targetBar.transform) {

			Destroy (child.gameObject);

		}

	}

	void RedrawOptions () {

		RemoveOptions ();

		if (ToddlerController.Instance.currentState == ToddlerState.Panicking) {
			carryingBar.DOFade (0f, 0.5f);
			spaceText.DOFade (0f, 0.5f);
			return;
		}
	 	
		List<GameObject> tools = ToddlerController.Instance.toolsInRange;

		for (int i = tools.Count - 1; i >= 0; i--) {

			GameObject option = Instantiate(optionPrefab);
			option.transform.SetParent (carryingBar.transform);

			if (option != null && !Equals(option, null)) {

				option.GetComponentInChildren<TextMeshProUGUI> ().text =
				(i + 1).ToString () + ") " + tools [i].transform.parent.gameObject.name;
			}
		}

		if (tools.Count == 0) {
			carryingBar.DOFade (0f, 0.5f);
			spaceText.DOFade (0f, 0.5f);
		}
		
	}


	void RedrawTargets () {
			
			
		RemoveTargetOptions ();

		if (ToddlerController.Instance.currentState == ToddlerState.Panicking) {
			plusBar.DOFade (0f, 0.5f);
			targetBar.DOFade (0f, 0.5f);
			spaceText.DOFade (0f, 0.5f);
			return;
		}
	 

		List<GameObject> targets = ToddlerController.Instance.reactingTargetsInRange;

		for (int i = targets.Count - 1; i >= 0; i--) {

			GameObject option = Instantiate(targetOptionPrefab);
			option.transform.SetParent (targetBar.transform);

			if (option != null && !Equals(option, null)) {

				option.GetComponentInChildren<TextMeshProUGUI> ().text =
				(i + 1).ToString () + ") " + targets [i].name;
			}
		}

		if (targets.Count == 0) {
			targetBar.DOFade (0f, 0.5f);
			plusBar.DOFade (0f, 0.5f);
			spaceText.DOFade (0f, 0.5f);
		}
		
		


	}

	void HideOptions () {


		RemoveOptions ();
		carryingBar.DOFade (0f, 0.5f);


	}
	
	
	void HideTargets () {


		RemoveTargetOptions ();
		targetBar.DOFade (0f, 0.5f);
		plusBar.DOFade (0f, 0.5f);


	}


	void FixedUpdate () {

		if (redraw) {

			Invoke ("RedrawOptions", 0.05f);
			redraw = false;
		}

		if (redrawTargets) {
			RedrawTargets ();
			redrawTargets = false;
		}
		

	}
	
    void OnObjectInRange (AbstractTool tool) {

		if (ToddlerController.Instance.isExploring ()) {

			redraw = true;

			if (carryingBar.alpha < 1f) {
				carryingBar.DOFade (1f, 0.5f);
			}


			if (spaceText.alpha < 1f) {
				spaceText.DOFade (1f, 0.5f);
			}


			spaceText.text = "Numeric Key = Pick Up";
			
		} else {

			if (ToddlerController.Instance.isCarrying() == false) {

				HideOptions ();

			}


		}
 
    }


	void OnCancelPickup (AbstractTool tool) {

		if (ToddlerController.Instance.isExploring ()) {
			redraw = true;

			if (ToddlerController.Instance.toolsInRange.Count == 0) {
				spaceText.DOFade (0f, 0.5f);
			}
		}
		
	}

	void OnCancelInteract (AbstractTool tool, AbstractTarget target) {


		redraw = true;
		redrawTargets = true;
		
		
	}
	
 
    void OnPickedUpObject (AbstractTool tool) {
 
        if (tool == null) {
           // carryingText.text = "Spanner";
        } else {
           // carryingText.text = tool.name;
        }

		// spaceText.DOFade (0f, 1f);

		RemoveOptions ();
		
	
		GameObject option = Instantiate(targetOptionPrefab);
		option.transform.SetParent (targetBar.transform);

		if (option != null && !Equals(option, null)) {

			option.GetComponentInChildren<TextMeshProUGUI> ().text =ToddlerController.CurrentTool.name;
		}
		
		
 
    }
     
    void OnDroppedObject (AbstractTool tool) {
        carryingBar.DOFade (0f, 1f);
        plusBar.DOFade (0f, 1f);
        targetBar.DOFade (0f, 1f);
        spaceText.DOFade (0f, 1f);

		//RemoveOptions ();
    }
     
    void OnApproachObject (AbstractTool tool, AbstractTarget target) {

		if (ToddlerController.Instance.isCarrying ()) {

			redrawTargets = true;

			if (targetBar.alpha < 1f) {
				targetBar.DOFade (1f, 0.5f);
			}


			if (spaceText.alpha < 1f) {
				spaceText.DOFade (1f, 0.5f);
			}
			
			if (plusBar.alpha < 1f) {
				plusBar.DOFade (1f, 0.5f);
			}


			spaceText.text = "Numeric Key = Interact";

		} else {
		
	
				HideTargets ();

			

		}

			//if (target == null) {
			//	targetText.text = "Microwave";
			//} else {
			//	targetText.text = target.name;
			//}

			//plusBar.alpha = 0;
			//plusBar.DOFade (1f, 1f);

			//targetBar.alpha = 0;
			//targetBar.DOFade (1f, 1f);

			//spaceText.alpha = 0;
			//spaceText.DOFade (1f, 1f);

			//spaceText.text = "Space = Interact";

    }
}