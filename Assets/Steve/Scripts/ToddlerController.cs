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
            else
                PickupTool();
        }
        else
        {
            //if (Input.GetAxisRaw("Horizontal") < 0)
            //{
            //    Walk(Direction.West);
            //}
            //else if (Input.GetAxisRaw("Horizontal") > 0)
            //{
            //    Walk(Direction.East);
            //}

            //if (Input.GetAxisRaw("Vertical") > 0)
            //{
            //    Walk(Direction.North);
            //}
            //else if (Input.GetAxisRaw("Vertical") < 0)
            //{
            //    Walk(Direction.South);
            //}

            TurnAndRotate();
        }
    }

    private void TurnAndRotate()
    {
        //float xMove = Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal") * GetWalkRate() * Time.deltaTime, limitsX.x, limitsX.y);
        //float yMove = Mathf.Clamp(transform.position.z + Input.GetAxis("Vertical") * GetWalkRate() * Time.deltaTime, limitsY.x, limitsY.y);

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
    /// Moves the toddler in the specified direction.
    /// </summary>
    /// <param name="direction">Direction.</param>
    private void Walk(Vector3 direction)
    {
        transform.Translate(direction * GetWalkRate() * Time.fixedDeltaTime);

        Vector3 newQ = new Vector3(0, 0, 0);

        if (direction == Direction.East)
        {
            newQ = new Vector3(0, 0, 0);
            //Debug.Log("East: " + newQ);
        }
        else if (direction == Direction.West)
        {
            newQ = new Vector3(0, 180f, 0);
            //Debug.Log("West: " + newQ);
        }
        else if (direction == Direction.North)
        {
            newQ = new Vector3(0, 90f, 0);
            //Debug.Log("North: " + newQ);
        }
        else if (direction == Direction.South)
        {
            newQ = new Vector3(0, -90f, 0);
            //Debug.Log("South: " + newQ);
        }

        //Debug.DrawRay(transform.position, newRotation, Color.red);

        //if (newRotation != Vector3.zero)
        //{
        //    Debug.Log("Direction: " + newRotation);
        //    //transform.rotation.SetLookRotation(newRotation);// = Quaternion.LookRotation(newRotation);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
        //    //Quaternion.RotateTowards(transform.rotation.)
        //}

        if (direction != Direction.Stationary)
        {
            //transform.eulerAngles = newQ;
            //transform.rotation = Quaternion.Euler(newQ);
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

    List<GameObject> availableTools = new List<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tool"))
        {
            availableTools.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Tool") && availableTools.Contains(collidedObject))
        {
            availableTools.Remove(collidedObject);
        }
    }

    public void PickupTool()
    {
        if (availableTools.Count == 1)
        {
            DoPickup(availableTools[0]);
        }
        else if (availableTools.Count > 1)
        {
            // prompt player to select a tool
        }
        else // no tools available
        {
            // do nothing
        }
    }

    public void DropTool()
    {
        if (CurrentTool != null)
        {
            CurrentTool.transform.SetParent(null);
            CurrentTool.GetComponent<Rigidbody>().useGravity = true;
            CurrentTool = null;

            Destroy(holdJoint);
        }
    }

    private void DoPickup(GameObject tool)
    {
        tool.transform.SetParent(this.transform);
        Vector3 toolSize = tool.GetComponent<BoxCollider>().size;
        tool.transform.localPosition = new Vector3(0, 0, radius + toolSize.z / 2);
        tool.GetComponent<Rigidbody>().useGravity = false;

        holdJoint = tool.AddComponent<FixedJoint>();
        holdJoint.breakForce = 10000f; // Play with this value
        holdJoint.connectedBody = this.rigidbody;

        CurrentTool = tool.GetComponent<AbstractTool>();
    }

    public static AbstractTool CurrentTool;
}
