using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Parent : AbstractTarget
{
    NavMeshAgent _agent;
    Transform _target;
    [SerializeField] float _updatePathThrottleSec;
    [SerializeField] GameObject _noisePrefab;
    float _lastTimePathUpdated;
    Animator _anim;

	GameObject randomTarget;

	bool randomWalking = true;

	public List<Transform> floors;
	
    void Awake()
    {
        Noise.OnEmitted += ReactToNoise;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

		randomTarget = new GameObject ();

		Invoke ("RandomRoom", 2f);
    }

    void ReactToNoise(Transform noiseTransform)
    {
        // feedback that parent is reacting to the noise.
        _target = noiseTransform;
        _anim.SetBool("isWalking", true);
        _agent.speed = 3.5f;
		_agent.angularSpeed = 220f;

		randomWalking = false;
		
    }

    [SerializeField] float _reactToToddlerDistance;

	void RandomRoom () {

		Vector3 position = floors [Random.Range (0, floors.Count)].position;

		randomTarget.transform.position = new Vector3 (position.x, 0f, position.y);

		_target = randomTarget.transform;
		_anim.SetBool ("isWalking", true);

		_agent.speed = 2f;
		_agent.angularSpeed = 120f;


	}

	void Update()
    {
		if (_target == null) {
			_agent.isStopped = true;
			_anim.SetBool ("isWalking", false);


			if (randomWalking && Random.Range (0, 500) == 1) {

				RandomRoom ();
			}

		}
		// TODO: return to idle cycle;

		else {
			if (Time.time >= _lastTimePathUpdated + _updatePathThrottleSec) {
				_agent.SetDestination (_target.position);
				_agent.isStopped = false;
			}

			//            print("distance to targeT: " + (Vector3.Distance(transform.position, _target.position)));

			if (Vector3.Distance (transform.position, _target.position) < _reactToToddlerDistance) {
				//                print("catch toddler");
				// catch the toddler
				if (randomWalking == false) {
					ToddlerController.Instance.OnCaught ();
					
				}
				_anim.SetBool ("isWalking", false);
				_agent.isStopped = true;
				_target = null;
				randomWalking = true;
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
