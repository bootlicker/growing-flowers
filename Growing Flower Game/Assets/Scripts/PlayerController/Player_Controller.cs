﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public float walkSpeed = 2;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    Animator animator;
    Transform cameraT;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
        cameraT = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
			
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        
        if (inputDir != Vector2.zero) {
            float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        
        float targetSpeed = (walkSpeed * inputDir.magnitude);
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        
        transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);
        
        float animationSpeedPercent = (.5f * inputDir.magnitude);
        
        animator.SetFloat("Speed Percent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}
}
