using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToddlerController : MonoBehaviour
{
    [SerializeField]
    private float walkRate = 1.0f;

    private CommandHandler commandHandler;

    private List<Vector3> currentDirections = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        this.commandHandler = new KeyboardCommandHandler();
        this.currentDirections.Add(Direction.Stationary);
    }

    private void Update()
    {
        UpdateCommands();
    }

    /// <summary>
    /// Upadtes the directions the toddler should move in (called from Update)
    /// </summary>
    private void UpdateCommands()
    {
        this.currentDirections.Clear();

        foreach (Command command in this.commandHandler.HandleInput())
        {
            command.Execute(this);
        }
    }

    private void FixedUpdate()
    {
        UpdatePhysics();
    }

    /// <summary>
    /// The current directions in which the toddler should move
    /// </summary>
    /// <value>The current directions.</value>
    public List<Vector3> CurrentDirections
    {
        get { return this.currentDirections; }
    }

    /// <summary>
    /// Updates the physics (called from FixedUpdate)
    /// </summary>
    private void UpdatePhysics()
    {
        foreach (Vector3 direction in CurrentDirections)
        {
            Walk(direction);
        }
    }

    /// <summary>
    /// Moves the toddler in the specified direction.
    /// </summary>
    /// <param name="direction">Direction.</param>
    private void Walk(Vector3 direction)
    {
        transform.Translate(direction * GetWalkRate() * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Gets the walk rate.
    /// </summary>
    /// <returns>The walk rate.</returns>
    private float GetWalkRate()
    {
        return this.walkRate;
    }
}
