
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class ExperienceController : MonoBehaviourSingleton<ExperienceController>

{

	private Dictionary<string, Dictionary<string, Vector2>> combinationLookup;
	private Dictionary<string, Vector2> interactLookup;
	

	public int currentLevel = 1;
	public float currentXP = 0f;
	public float newXP = 0f;
	
	public float currentLevelXPTarget = 30f;
	public float currentLevelXPFloor = 0f;

	public float currentAnguish = 0f;
	public float newAnguish = 0f;
	
	
	public float anguishMax = 250f;
	public float anguishReducePerFrame = 0.01f;

	public float barWidthMax = 402f;

	public float addPerFrame = 1f;

	public float levelIncreaseMultiplier = 1.5f;
	public float repeatExperienceReduceFactor = 0.5f;
	public float repeatAnguishIncreaseFactor = 1.3f;

	public List<string> anguishLevels;

	
	public TextMeshProUGUI experienceLevelText;
	public TextMeshProUGUI anguishLevelText;

	public RectTransform experienceBar;
	public RectTransform anguishBar;
	
    // Start is called before the first frame update
    void Start()
    {

		combinationLookup = new Dictionary<string, Dictionary<string, Vector2>>();
		interactLookup = new Dictionary<string, Vector2> ();
		
		
		// FOR EACH TOOL, IT HAS A DICTIONARY OF TARGETS IT CAN REACT WITH

		combinationLookup.Add ("Spoon", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Fork", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Cup", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Plate", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Foil", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Paper", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Toy", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Biscuit", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Drawing", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Bleach", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Felt Tip", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Bottle", new Dictionary<string, Vector2> ());
		combinationLookup.Add ("Remote", new Dictionary<string, Vector2> ());
		
		combinationLookup ["Spoon"].Add ("Plate", new Vector2 (10, 20));
		combinationLookup ["Spoon"].Add ("Microwave", new Vector2 (40, 60));
		combinationLookup ["Spoon"].Add ("Jinglebell", new Vector2 (15, 25));
		combinationLookup ["Spoon"].Add ("Tambourine", new Vector2 (20, 15));
		combinationLookup ["Spoon"].Add ("Blender", new Vector2 (30, 40));
		combinationLookup ["Spoon"].Add ("Socket", new Vector2 (40, 50));
		
		combinationLookup ["Fork"].Add ("Plate", new Vector2 (5, 30));
		combinationLookup ["Fork"].Add ("Microwave", new Vector2 (40, 60));
		combinationLookup ["Fork"].Add ("Jinglebell", new Vector2 (5, 10));
		combinationLookup ["Fork"].Add ("Tamborine", new Vector2 (10, 10));
		combinationLookup ["Fork"].Add ("Blender", new Vector2 (30, 40));
		combinationLookup ["Fork"].Add ("Socket", new Vector2 (30, 40));
		
		combinationLookup ["Cup"].Add ("Blender", new Vector2 (15, 25));
		
		combinationLookup ["Plate"].Add ("Tambourine", new Vector2 (15, 15));
		combinationLookup ["Plate"].Add ("Blender", new Vector2 (10, 30));
		
		combinationLookup ["Foil"].Add ("Microwave", new Vector2 (40, 50));
		combinationLookup ["Foil"].Add ("Blender", new Vector2 (30, 10));
		combinationLookup ["Foil"].Add ("Oven", new Vector2 (20, 10));
		
		combinationLookup ["Paper"].Add ("Blender", new Vector2 (20, 10));
		combinationLookup ["Paper"].Add ("Oven", new Vector2 (15, 40));
		
		combinationLookup ["Toy"].Add ("Blender", new Vector2 (0, 40));
		combinationLookup ["Toy"].Add ("Parent", new Vector2 (15, -80));
		
		combinationLookup ["Biscuit"].Add ("Blender", new Vector2 (15, 30));
		combinationLookup ["Biscuit"].Add ("Parent", new Vector2 (5, -100));
		
		combinationLookup ["Drawing"].Add ("Blender", new Vector2 (15, 30));
		combinationLookup ["Drawing"].Add ("Parent", new Vector2 (30, -150));
		
		combinationLookup ["Bleach"].Add ("Blender", new Vector2 (15, 60));
		
		combinationLookup ["Felt Tip"].Add ("Sofa", new Vector2 (20, 40));
		
		combinationLookup ["Remote"].Add ("TV", new Vector2 (35, 25));
		combinationLookup ["Remote"].Add ("Bedroom TV", new Vector2 (100, 80));


		interactLookup.Add ("Speak", new Vector2 (10, -20));
		interactLookup.Add ("Sing", new Vector2 (15, -30));
		interactLookup.Add ("Cuddle", new Vector2 (5, -40));
		interactLookup.Add ("Dance", new Vector2 (20, -50));
		
		
		
		
		
    }
    
    
    [Button ("Add Random Experience")]
    void AddExperience () {

		if (newXP == currentXP) {
			UpdateExperience (Random.Range (5, 30));
		}
 
    }
    
     [Button ("Add Random Anguish")]
    void AddAnguish () {

		//if (newAnguish == currentAnguish) {
			UpdateAnguish (Random.Range (-20, 60));
		//}
 
    }
    

    // Update is called once per frame
    void Update()
    {

		CheckExperience ();
		CheckAnguish ();

    }


	void CheckExperience () {

		if (newXP > currentXP) {
		
			currentXP = Mathf.Clamp (currentXP + addPerFrame, currentXP, newXP);
			
			if (currentXP >= currentLevelXPTarget) {
				NewLevel ();
			}

			UpdateExperienceBar ();
		}


	}


	void CheckAnguish () {



		if (newAnguish != currentAnguish) {

			if (currentAnguish > newAnguish) {
				currentAnguish = Mathf.Max (0f, currentAnguish -= addPerFrame);
			} else {
				currentAnguish = Mathf.Min (currentAnguish += addPerFrame, anguishMax);
			}

		} else {

			currentAnguish = Mathf.Max (0f, currentAnguish -= anguishReducePerFrame);
			newAnguish = currentAnguish;
		}

		if (currentAnguish >= anguishMax) {
			Debug.LogError ("GAME OVER!!!!!!!!!!!");
			
			

		}
		
		UpdateAnguishBar ();

	}

	

	void UpdateExperienceBar () {

		float percBar = (currentXP - currentLevelXPFloor) / (currentLevelXPTarget - currentLevelXPFloor);
		experienceBar.sizeDelta = new Vector2 (percBar * barWidthMax, experienceBar.sizeDelta.y);

	}
	
	void UpdateAnguishBar () {

		float percBar = (currentAnguish / anguishMax);
		anguishBar.sizeDelta = new Vector2 (percBar * barWidthMax, anguishBar.sizeDelta.y);

		int labelIndex = (int)Mathf.Floor (((currentAnguish / anguishMax * 7f) - 0f));

		labelIndex = Mathf.Clamp (labelIndex, 0, 7);

		//Debug.Log (labelIndex + " " + currentAnguish + " / " + anguishMax);
		
		anguishLevelText.text = anguishLevels [labelIndex];
		

	}

	public void Caught () {
	
		UpdateAnguish (50f);
	}
	
	public void UpdateExperienceAndAnguish (string targetName, string toolName) {

		if (targetName == "Caught") {
			Caught ();
			return;
		}

		Vector2 reaction = Vector2.zero;

		if (combinationLookup.ContainsKey (toolName) == false) {
		
			Debug.Log (toolName + " does not have an entry in the experience dictionary (wrong name?)");

			reaction = new Vector2 (3, 0);
		}
		
		else {

			if (combinationLookup [toolName].ContainsKey (targetName) == false) {

				Debug.Log (toolName + " does not have specific experience/anguish with this " + targetName);

				if (interactLookup.ContainsKey (toolName) == false) {
				
					Debug.Log (toolName + " does not have a default experience entry with no target");
					reaction = new Vector2 (3, 0);
					
				} else {

					reaction = interactLookup [toolName];

				}

			} else {

				reaction = combinationLookup [toolName] [targetName];
				combinationLookup [toolName] [targetName] =

				new Vector2 (
					combinationLookup [toolName] [targetName].x * repeatExperienceReduceFactor,

					Mathf.Max (combinationLookup [toolName] [targetName].y * repeatAnguishIncreaseFactor,
							combinationLookup [toolName] [targetName].y));
						
				

			}
			
		}

		UpdateExperience (reaction.x);
		UpdateAnguish (reaction.y);

			
	}
	
	void UpdateAnguish (float ang) {

		newAnguish = Mathf.Clamp(currentAnguish + ang, 0f, anguishMax);

	}


	void NewLevel () {

		currentLevel++;



		float newLevelAmount = (currentLevelXPTarget - currentLevelXPFloor) * levelIncreaseMultiplier;
		
		currentLevelXPFloor = currentLevelXPTarget;
		
		currentLevelXPTarget = currentLevelXPTarget + newLevelAmount;
		
		experienceLevelText.text = "LVL " + currentLevel;
	
		
	}


	void UpdateExperience (float xp) {

		newXP = currentXP + xp;
		
	
	}
	
	
	
		
    
}
