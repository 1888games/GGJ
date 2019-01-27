
using System;
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
 
 
    void Awake () {
		// listen to tools and targets state changes

		//

		ToddlerController.OnPickedUp += OnPickedUpObject;
		ToddlerController.OnDropped += OnDroppedObject;
		ToddlerController.PickupAttempt += OnAttemptPickup;
		ToddlerController.OnWalkaway += OnCancelPickup;

		ToddlerController.OnChaseModeFinished += OnChaseFinished;
		
		

		AbstractTarget.InteractionAttempt += OnApproachObject;
		AbstractTarget.OnWalkaway += OnCancelInteract;
		AbstractTarget.ConfirmationReceived += CancelAll;
	
		
		
    }
 
    void UpdateUI () {
 
    }
 
    [Button ("test pickup")]
    void TestPickup () {
 
        OnPickedUpObject (null);
 
    }

	void OnChaseFinished () {

		spaceText.DOFade (0f, 1f);
		carryingBar.DOFade (0f, 1f);
		targetBar.DOFade (0f, 1f);
        plusBar.DOFade (0f, 1f);


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
 
        OnAttemptPickup (null);
 
    }
 
 
    void OnAttemptPickup (AbstractTool tool) {
 
         
        if (tool == null) {
            carryingText.text = "Spoon";
        } else {
            carryingText.text = tool.name;
        }
 
        carryingBar.alpha = 0;
        carryingBar.DOFade (1f, 1f);
         
        spaceText.alpha = 0;
        spaceText.DOFade (1f, 1f);
        spaceText.text = "Space = Pick Up";
 
 
    }


	void OnCancelPickup (AbstractTool tool) {

		spaceText.DOFade (0f, 1f);
		carryingBar.DOFade (0f, 1f);

	}

	void OnCancelInteract (AbstractTool tool, AbstractTarget target) {
			
		spaceText.DOFade (0f, 1f);
		targetBar.DOFade (0f, 1f);
		plusBar.DOFade (0f, 1f);
      
	}


	void CancelAll (AbstractTool tool, AbstractTarget target) {

		spaceText.DOFade (0f, 1f);
		carryingBar.DOFade (0f, 1f);
		targetBar.DOFade (0f, 1f);
        plusBar.DOFade (0f, 1f);

	}
 
    void OnPickedUpObject (AbstractTool tool) {
 
        if (tool == null) {
            carryingText.text = "Spanner";
        } else {
            carryingText.text = tool.name;
        }
 
        spaceText.DOFade (0f, 1f);
 
    }
     
    void OnDroppedObject (AbstractTool tool) {
 
        carryingBar.DOFade (0f, 1f);
        plusBar.DOFade (0f, 1f);
        targetBar.DOFade (0f, 1f);
        spaceText.DOFade (0f, 1f);
 
    }
     
    void OnApproachObject (AbstractTool tool, AbstractTarget target) {
         
        if (target == null) {
            targetText.text = "Microwave";
        } else {
            targetText.text = target.name;
        }
 
        plusBar.alpha = 0;
        plusBar.DOFade (1f, 1f);
         
        targetBar.alpha = 0;
        targetBar.DOFade (1f, 1f);
         
        spaceText.alpha = 0;
        spaceText.DOFade (1f, 1f);
        spaceText.text = "Space = Interact";
 
    }
}