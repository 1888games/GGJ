using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToddlerController : MonoBehaviour
{
    [SerializeField]
    private float walkRate = 1.0f;

    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidbody;
    private FixedJoint holdJoint;

    private float radius;
    private float height;

    private bool waitingForConfirmation;

    private GameObject availableTool;

    // Start is called before the first frame update
    void Start()
    {
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
            availableTool = other.gameObject;
            PickupAttempt(availableTool.GetComponent<AbstractTool>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tool") && availableTool == other.gameObject)
            availableTool = null;
    }

    private void DropTool()
    {
        if (CurrentTool != null)
        {
            OnDropped(CurrentTool);

            CurrentTool.transform.SetParent(null);
            CurrentTool.GetComponent<Rigidbody>().useGravity = true;
            CurrentTool = null;

            Destroy(holdJoint);
        }
    }

    private void DoPickup()
    {
        if (availableTool != null)
        {
            availableTool.transform.SetParent(this.transform);
            Vector3 toolSize = availableTool.GetComponent<BoxCollider>().size;
            availableTool.transform.localPosition = new Vector3(0, 0, radius + toolSize.z / 2);
            availableTool.GetComponent<Rigidbody>().useGravity = false;

            holdJoint = availableTool.AddComponent<FixedJoint>();
            holdJoint.breakForce = 10000f; // Play with this value
            holdJoint.connectedBody = this.rigidbody;

            CurrentTool = availableTool.GetComponent<AbstractTool>();

            OnPickedUp(CurrentTool);
        }
    }

    public static AbstractTool CurrentTool;

    public static Action<AbstractTool> PickupAttempt;
    public static Action<AbstractTool> OnPickedUp;
    public static Action<AbstractTool> OnDropped;
}
