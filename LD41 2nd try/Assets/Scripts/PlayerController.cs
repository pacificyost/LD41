using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    private float settledThreshold = 0.01f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool Settled()
    {
        return Mathf.Abs(rb.velocity.magnitude) < settledThreshold;
        
    }
}
