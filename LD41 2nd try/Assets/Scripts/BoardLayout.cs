using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLayout : MonoBehaviour {
    public GameObject pinPrefab;
    public int pinCount = 100;
    public float minX = -1.0f;
    public float maxX = 1.0f;
    public float minY = -1.0f;
    public float maxY = 1.0f;

	// Use this for initialization
	void Start () {
        BuildLayout();
	}

    public void BuildLayout()
    {
        for (int i = 0; i < pinCount; i++)
        {
            Vector3 pinLocation = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            GameObject newPin = GameObject.Instantiate(pinPrefab, pinLocation, pinPrefab.transform.rotation, transform);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
