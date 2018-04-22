using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    GameObject player;
    public Vector3 cameraOffset;
    public float cameraSpeed = 1.0f;
    public float cameraDeadZone = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null && (Mathf.Abs(transform.position.x - player.transform.position.x) > cameraDeadZone || Mathf.Abs(transform.position.y - player.transform.position.y) > cameraDeadZone))
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset, cameraSpeed * Time.deltaTime);
        }
	}
}
