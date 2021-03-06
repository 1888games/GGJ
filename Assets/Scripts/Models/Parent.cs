﻿using UnityEngine;
using UnityEngine.AI;

public class Parent : AbstractTarget
{
    NavMeshAgent _agent;
    Transform _target;
    [SerializeField] float _updatePathThrottleSec;
    [SerializeField] GameObject _noisePrefab;
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

//            print("distance to targeT: " + (Vector3.Distance(transform.position, _target.position)));

            if (Vector3.Distance(transform.position, _target.position) < _reactToToddlerDistance)
            {
//                print("catch toddler");
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

    public override void React()
    {
        if (IsReactableTool())
        {
            Amuse();
        }
    }

    private void Amuse()
    {
        print("Parent success reaction.");
        Animation animation = GetComponent<Animation>();
        if (animation != null)
            animation.Play();
        Fabric.EventManager.Instance.PostEvent("Broken_Blender", Fabric.EventAction.PlaySound, null, gameObject);
        Instantiate(_noisePrefab, transform.position, Quaternion.identity);
    }
}
