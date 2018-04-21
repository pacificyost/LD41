using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    PlayerController pc;
    BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
        
        boxCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (pc == null)
        {
            pc = GameObject.FindObjectOfType<PlayerController>();
        }
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
