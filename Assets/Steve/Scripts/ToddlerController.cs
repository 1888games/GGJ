using System;
using System.Collections;
using System.Runtime.InteropServices;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = System.Random;

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

    private bool waitingForConfirmation;

    private GameObject availableTool;
    GameObject currentNoise;
    string currentParentAction;
    
    string[] ParentActions = {"Speak", "Sing", "Cuddle", "Dance"};

    bool isBeingChased;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        radius = capsuleCollider.radius;
        height = capsuleCollider.height;
    }

    private void Update()
    {
        if (!Input.anyKey)
        {
            // do nothing
            _anim.SetBool("isWalking", false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (CurrentTool != null)
                DropTool();
        }
        else if (waitingForConfirmation && Input.GetKeyDown(KeyCode.Space))
        {
            waitingForConfirmation = false;

            DoPickup();
        }
        else
        {
            TurnAndRotate();
            _anim.SetBool("isWalking", true);
        }
    }

    private void TurnAndRotate()
    {
        float xMove = transform.position.x + Input.GetAxis("Horizontal") * GetWalkRate() * Time.fixedDeltaTime;
        float yMove = transform.position.z + Input.GetAxis("Vertical") * GetWalkRate() * Time.fixedDeltaTime;

        // rotate to face direction of travel
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.eulerAngles = new Vector3(0f, Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg, 0f);

            transform.position = (new Vector3(xMove, transform.position.y, yMove));
        }
        
    }

    /// <summary>
    /// Gets the walk rate.
    /// </summary>
    /// <returns>The walk rate.</returns>
    private float GetWalkRate()
    {
        return this.walkRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!waitingForConfirmation && other.gameObject.CompareTag("Tool"))
        {
            waitingForConfirmation = true;
            availableTool = other.transform.parent.gameObject;
            PickupAttempt(availableTool.GetComponent<AbstractTool>());
        }

//        else if (other.gameObject.CompareTag("Parent"))
//        {
//            waitingForConfirmation = true;
//            currentParentAction = ParentActions[UnityEngine.Random.Range(0, ParentActions.Length)];
//            OnParentApproached(currentParentAction);
//        }
    }

    
    
    [Button("Action mode")]
    public void OnMadeNoise()
    {
        Fabric.EventManager.Instance.PostEvent("Music", Fabric.EventAction.SetSwitch, "ActionLoop");
        _anim.SetBool("isPanicking", true);
        // enter chase mode.
        currentNoise = Instantiate(_noisePrefab, transform);
        currentNoise.transform.localPosition = Vector3.zero;
        isBeingChased = true;
        StartCoroutine(ChaseCountdown());
    }

    
    IEnumerator ChaseCountdown()
    {
        int secondsRemaining = 10;
        _countdown.alpha = 1f;
        while (secondsRemaining != 0)
        {
            if (isBeingChased == false)
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
        isBeingChased = false;
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
        isBeingChased = false;
        Destroy(currentNoise);
        _anim.SetBool("isPanicking", false);
        Fabric.EventManager.Instance.PostEvent("Music", Fabric.EventAction.SetSwitch, "ExploLoop");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tool") && availableTool == other.transform.parent.gameObject)
        {
            OnWalkaway(availableTool.GetComponent<AbstractTool>());
            availableTool = null;
        }
    }

    private void DropTool()
    {
        if (CurrentTool != null)
        {
            OnDropped(CurrentTool);
            CurrentTool.transform.SetParent(null);
            CurrentTool.GetComponent<Rigidbody>().useGravity = true;
			availableTool.GetComponent<BoxCollider> ().enabled = true;
            CurrentTool = null;

            Destroy(holdJoint);
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
        
        print("caught");
        isBeingChased = false;
        OnChaseModeFinish();
        StartCoroutine(caught());
        // stop countdown.;
        // destroy tool.
        // trigger xp change.
    }

    
    private void DoPickup()
    {
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
