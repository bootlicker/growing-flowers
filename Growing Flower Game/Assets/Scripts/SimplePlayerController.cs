using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float maxVelocity = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (transform.GetComponent<Rigidbody>().velocity.magnitude < maxVelocity)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.back * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.right * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.left * speed);
            }
        }
    }
}
