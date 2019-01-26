using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ToolAndTargetUI : MonoBehaviour
{

	public CanvasGroup carryingBar;
	public CanvasGroup targetBar;
	public CanvasGroup plusBar;
	
	public TextMeshProUGUI carryingText;
	public TextMeshProUGUI targetText;

    
    void Awake()
    {
        // listen to tools and targets state changes
        
       	//
    }
    
    void UpdateUI()
    {
        
    }

    [Button("test pickup")]
    void TestPickup()
    {

		OnPickedUpObject (null, "Crayon");
       
    }

	[Button("test drop")]
    void TestDrop()
    {

		OnDroppedObject (null);
       
    }
    
    [Button("test approach")]
    void TestApproach()
    {

		OnApproachObject (null, "Microwave");
       
    }



	void OnPickedUpObject (AbstractTool tool, string name = null) {

		if (tool == null) {
			carryingText.text = name;
		} else {
			carryingText.text = tool.name;
		}

		carryingBar.alpha = 0;
		carryingBar.DOFade (1f, 1f);

	}
	
	void OnDroppedObject (AbstractTool tool) {

		carryingBar.DOFade (0f, 1f);
		plusBar.DOFade (0f, 1f);
		targetBar.DOFade (0f, 1f);

	}
	
	void OnApproachObject (AbstractTarget target, string name = null) {
		
		if (target == null) {
			targetText.text = name;
		} else {
			targetText.text = target.name;
		}

		plusBar.alpha = 0;
		plusBar.DOFade (1f, 1f);
		
		targetBar.alpha = 0;
		targetBar.DOFade (1f, 1f);

	}
}
