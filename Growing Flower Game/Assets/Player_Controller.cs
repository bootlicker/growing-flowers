using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    public float walkSpeed = 2;
    
    Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
			
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Vector2 inputDir = input.normalized;
        
        if (inputDir != Vector2.zero) {
        
        transform.eulerAngles = Vector3.up * Mathf.Atan(inputDir.x/inputDir.y) * Mathf.Rad2Deg;
        }
        
        float speed = (walkSpeed * inputDir.magnitude);
        
        transform.Translate (transform.forward * speed * Time.deltaTime, Space.World);
        
        float animationSpeedPercent = (.5f * inputDir.magnitude);
        
        animator.SetFloat("Speed Percent", animationSpeedPercent);
	}
}
