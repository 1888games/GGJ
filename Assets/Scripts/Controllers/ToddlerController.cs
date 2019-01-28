using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ToddlerState { Exploring, Carrying, Panicking }

public class ToddlerController : MonoBehaviourSingleton<ToddlerController>
{
    [SerializeField]
    private float walkRate = 1.0f;

    [SerializeField] GameObject _noisePrefab;
    [SerializeField] CanvasGroup _countdown;
    [SerializeField] TextMeshProUGUI _countdownText;
    [SerializeField] Transform _hand;
    [SerializeField] GameObject _chaseAvoided;
    [SerializeField] GameObject _caught;
         
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidbody;
    private FixedJoint holdJoint;
    Animator _anim;
    private float radius;
    private float height;

    GameObject currentNoise;
    string currentParentAction;
    
    string[] ParentActions = {"Speak", "Sing", "Cuddle", "Dance"};
    
    public ToddlerState currentState;
	public List<GameObject> toolsInRange;
	public List<GameObject> targetsInRange;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        radius = capsuleCollider.radius;
        height = capsuleCollider.height;

		currentState = ToddlerState.Exploring;

		toolsInRange = new List<GameObject> ();
		targetsInRange = new List<GameObject> ();
        
    }

	public bool isCarrying () {
		return currentState == ToddlerState.Carrying;
	}
	
	public bool isExploring () {
		return currentState == ToddlerState.Exploring;
	}
	
	public bool isPanicking () {
		return currentState == ToddlerState.Panicking;
	}

    private void Update()
    {
        if (!Input.anyKey)
        {
            _anim.SetBool("isWalking", false);
			return;
        }
        
        if (Input.GetKeyDown(KeyCode.RightShift) && currentState == ToddlerState.Carrying)
        {
			DropTool ();
			return;
			
        }

		int numericKey = 99;

		for (int i = 1; i < 10; ++i) {
			if (Input.GetKeyDown ("" + i)) {
				numericKey = i;
			}
		}

		if (currentState == ToddlerState.Exploring && toolsInRange.Count >= numericKey)
        {
       
            DoPickup(numericKey - 1);
			return;
        }
        
        
		TurnAndRotate();
        
        if(_anim.GetBool("isPanicking") == false) _anim.SetBool("isWalking", true);
        
    }

    private void TurnAndRotate()
    {
        float xMove = transform.position.x + Input.GetAxis("Horizontal") * walkRate * Time.fixedDeltaTime;
        float yMove = transform.position.z + Input.GetAxis("Vertical") * walkRate * Time.fixedDeltaTime;

        // rotate to face direction of travel
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.eulerAngles = new Vector3(0f, Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg, 0f);

            transform.position = (new Vector3(xMove, transform.position.y, yMove));
            if(_anim.GetBool("isPanicking") == false) _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }
        
    }



    [Button("Reload game")]
    void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    
    
    [Button("Action mode")]
    public void OnMadeNoise()
    {
        Fabric.EventManager.Instance.PostEvent("Music", Fabric.EventAction.SetSwitch, "ActionLoop");
        _anim.SetBool("isWalking", false);
        _anim.SetBool("isPanicking", true);
        // enter chase mode.
        currentNoise = Instantiate(_noisePrefab, transform);
        currentNoise.transform.localPosition = Vector3.zero;
        currentState = ToddlerState.Panicking;
        StartCoroutine(ChaseCountdown());
    }

    
    IEnumerator ChaseCountdown()
    {
        int secondsRemaining = 10;
        _countdown.alpha = 1f;
        while (secondsRemaining != 0)
        {
            if (currentState != ToddlerState.Panicking)
            {
                // got caught.
                _countdown.alpha = 0f;
                yield break;
            }
            _countdownText.text = secondsRemaining.ToString();
            yield return new WaitForSeconds(1);
            secondsRemaining--;
        }
        OnChaseModeFinish();
        StartCoroutine(chaseAvoided());
    }

    
    IEnumerator chaseAvoided()
    {
        _chaseAvoided.SetActive(true);
        yield return new WaitForSeconds(2f);
        _chaseAvoided.SetActive(false);
    }
    
    IEnumerator caught()
    {
        currentState = ToddlerState.Exploring;
        _caught.SetActive(true);
        yield return new WaitForSeconds(2f);
        _caught.SetActive(false);
        OnChaseModeFinish();
    }

    public static Action OnChaseModeFinished = delegate {  };
    void OnChaseModeFinish()
    {
        OnChaseModeFinished();
        _countdown.alpha = 0f;
		currentState = ToddlerState.Exploring;
        Destroy(currentNoise);
        _anim.SetBool("isPanicking", false);
        Fabric.EventManager.Instance.PostEvent("Music", Fabric.EventAction.SetSwitch, "ExploLoop");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag ("Tool") && toolsInRange.Contains (other.gameObject))  
        {

			OnWalkaway (other.gameObject.GetComponent<AbstractTool> ());
			
            toolsInRange.Remove (other.gameObject);
            Debug.Log ("TOOLS IN RANGE REMOVE: " + toolsInRange.Count);
           
        }
    }
    
    private void OnTriggerStay(Collider other)
    {

		if (other.gameObject.CompareTag ("Tool") && toolsInRange.Contains (other.gameObject) == false) {

			AbstractTool tool = other.transform.GetComponent<AbstractTool> ();

			toolsInRange.Add (other.gameObject);
			PickupAttempt(tool);

			Debug.Log ("TOOLS IN RANGE ADD: " + toolsInRange.Count);

		}
		
        
    }


    private void DropTool()
    {
        if (CurrentTool != null)
        {
            OnDropped(CurrentTool);
            CurrentTool.transform.SetParent(null);
            CurrentTool.GetComponent<Rigidbody>().useGravity = true;
			CurrentTool.GetComponent<BoxCollider> ().enabled = true;
            CurrentTool = null;

            Destroy(holdJoint);

			currentState = ToddlerState.Exploring;
        }
    }

    public void DestroyTool()
    {
        if (CurrentTool == null)
        {
            print("trying to destroy a tool but it's null");
            return;
        }
        Destroy(CurrentTool.gameObject);
        CurrentTool = null;
    }

    public void OnCaught()
    {
        currentState = ToddlerState.Exploring;
        OnChaseModeFinish();
        StartCoroutine(caught());
        ExperienceController.Instance.UpdateExperienceAndAnguish("Caught",null);
        Fabric.EventManager.Instance.PostEvent("Jingle_Neg", Fabric.EventAction.PlaySound, null, gameObject);
    }

    
    private void DoPickup(int id)
    {

	
		
		GameObject availableTool = toolsInRange [id].transform.parent.gameObject;
		
		toolsInRange.RemoveAt (id);
		
        if (availableTool != null)
        {
        
        	
            availableTool.transform.SetParent(_hand == null ? transform : _hand);
            Vector3 toolSize = availableTool.GetComponent<BoxCollider>().size;
            availableTool.transform.localPosition = new Vector3(0, 0, radius + toolSize.z / 2);
            availableTool.GetComponent<Rigidbody>().useGravity = false;
			availableTool.GetComponent<BoxCollider> ().enabled = false;

            holdJoint = availableTool.AddComponent<FixedJoint>();
            holdJoint.breakForce = 10000f; // Play with this value
            holdJoint.connectedBody = this.rigidbody;

            CurrentTool = availableTool.GetComponent<AbstractTool>();
            OnWalkaway (CurrentTool);
			currentState = ToddlerState.Carrying;
            OnPickedUp(CurrentTool);
        }
    }

    public static AbstractTool CurrentTool;

    public static Action<string> OnParentApproached = delegate {  };
    public static Action<AbstractTool> PickupAttempt= delegate {  };
    public static Action<AbstractTool> OnWalkaway = delegate { };
    public static Action<AbstractTool> OnPickedUp= delegate {  };
    public static Action<AbstractTool> OnDropped= delegate {  };
}
