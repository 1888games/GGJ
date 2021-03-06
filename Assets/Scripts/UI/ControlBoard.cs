﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBoard : MonoBehaviour

	
{

	public float rotateSpeed = 0.05f;
	public float minRotation = -50f;
	public float maxRotation = 50f;
	float mult = 0f;
	float newY;
	float oldY;

	float currentRotateSpeed = 0f;

	public float damping = 0.05f;

	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update () {

		//mult = 0f;
		
		if (Input.GetKey (KeyCode.Comma)) {
			mult = -1f;
		}

		if (Input.GetKey (KeyCode.Period)) {
			mult = 1f;
		}

		if (mult > 0f) {
			mult = Mathf.Clamp(mult - damping,0f,mult);
		}
		
		if (mult < 0f) {
			mult = Mathf.Clamp(mult + damping,mult,0f);
		}

		if (mult != 0f) {

			oldY = transform.localEulerAngles.y;

			if (oldY > 180f) {
				oldY = oldY - 360f;
			}
			
			newY = Mathf.Clamp (oldY + (rotateSpeed * mult), minRotation, maxRotation);

		//Debug.Log (transform.localEulerAngles.y + " - " + newY);

			if (newY > 180f) {
				newY = newY - 360f; 
			}
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, newY, transform.localEulerAngles.z);
		}
	}
}
