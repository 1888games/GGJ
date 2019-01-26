using UnityEngine;
using UnityEngine.AI;

public class Parent : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _target;
    [SerializeField] float _updatePathThrottleSec;
    float _lastTimePathUpdated;

    void Awake()
    {
        Noise.OnEmitted += ReactToNoise;
        _agent = GetComponent<NavMeshAgent>();
    }

    void ReactToNoise(Transform noiseTransform)
    {
        // feedback that parent is reacting to the noise.
        _target = noiseTransform;
    }
    
    void Update()
    {
        if (_target == null)
        {
            _agent.isStopped = true;
            // TODO: return to idle cycle;
        }
        else
        {            
            if (Time.time >= _lastTimePathUpdated + _updatePathThrottleSec)
            {
                _agent.SetDestination(_target.position);
                _agent.isStopped = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Noise>() != null)
        {
            // we've reached the source of the noise.
            _target = null;
        }
    }
}
