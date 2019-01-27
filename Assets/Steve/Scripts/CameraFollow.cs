using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private Vector3 offset;

	public float zoomOutSize = 5f;
	public float zoomInSize = 2.75f;

	public float targetSize = 2.75f;

	public float sizePerFrame = 0.01f;

	public Vector3 lastPlayerPosition;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

		targetSize = zoomInSize;
		Camera.main.orthographicSize = zoomOutSize;

		lastPlayerPosition = player.transform.position;
		
    }

	void Update () {

		Camera c = Camera.main;

		if (lastPlayerPosition != player.transform.position) {
			targetSize = zoomOutSize;
		} else {
			targetSize = zoomInSize;
		}

		if (c.orthographicSize > targetSize) {
			c.orthographicSize = Mathf.Clamp (c.orthographicSize - sizePerFrame, targetSize, c.orthographicSize);
		}

		if (c.orthographicSize < targetSize) {
			c.orthographicSize = Mathf.Clamp (c.orthographicSize + sizePerFrame, c.orthographicSize, targetSize);
		}

		lastPlayerPosition = player.transform.position;


	}
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;

    }
}
