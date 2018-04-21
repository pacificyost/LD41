using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    PlayerController pc;
    BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
        pc = GameObject.FindObjectOfType<PlayerController>();
        boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Entered");
        if (pc.Settled())
        {
            Debug.Log("Settled");
            boxCollider.enabled = false;
        }
    }
}
