using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        Vector2 inputDir = input.normalized;
        
        transform.eulerAngles = Vector3.up * Mathf.Atan(inputDir.x/inputDir.y) * Mathf.Rad2Deg;
	}
}
