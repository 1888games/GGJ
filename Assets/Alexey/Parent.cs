using UnityEngine;
using UnityEngine.AI;

public class Parent : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _target;
    [SerializeField] float _updatePathThrottleSec;
    float _lastTimePathUpdated;
    Animator _anim;
    void Awake()
    {
        Noise.OnEmitted += ReactToNoise;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void ReactToNoise(Transform noiseTransform)
    {
        // feedback that parent is reacting to the noise.
        _target = noiseTransform;
        _anim.SetBool("isWalking", true);
    }

    [SerializeField] float _reactToToddlerDistance;
    
    void Update()
    {
        if (_target == null)
        {
            _agent.isStopped = true;
            _anim.SetBool("isWalking", false);
            // TODO: return to idle cycle;
        }
        else
        {            
            if (Time.time >= _lastTimePathUpdated + _updatePathThrottleSec)
            {
                _agent.SetDestination(_target.position);
                _agent.isStopped = false;
            }

            if (Vector3.Distance(transform.position, _target.position) < _reactToToddlerDistance)
            {
                // catch the toddler
                ToddlerController.Instance.OnCaught();
                _anim.SetBool("isWalking", false);
                _agent.isStopped = true;
                _target = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // catch the toddler.
        if (other.GetComponent<Noise>() != null)
        {
            // we've reached the source of the noise.
            _target = null;
            _anim.SetBool("isWalking", false);
        }
    }
}
