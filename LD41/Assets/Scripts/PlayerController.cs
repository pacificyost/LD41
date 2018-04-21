using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;
    public float distToTarget = 0.5f;
    private Transform nextTurningPoint;
    private TurnDirection turnDirection;
    private Rigidbody rb;

    private Vector3 leftVelocity;
    private Vector3 rightVelocity;
    private Vector3 fowardVelocity;
    private Vector3 backwardVelocity;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        InitializeVelocities();
        SetVelocity(TurnDirection.right);
    }

    private void InitializeVelocities()
    {
        leftVelocity = new Vector3(-1 * speed, 0, 0);
        rightVelocity = new Vector3(speed, 0, 0);
        fowardVelocity = new Vector3(0, 0, speed);
        backwardVelocity = new Vector3(0, 0, -1 * speed);
    }

    private void SetVelocity(TurnDirection direction)
    {
        switch (direction)
        {
            case TurnDirection.right:
                {
                    rb.velocity = rightVelocity;
                    break;
                }
            case TurnDirection.left:
                {
                    rb.velocity = leftVelocity;
                    break;
                }
            case TurnDirection.foward:
                {
                    Debug.Log("Foward " + fowardVelocity);
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                    rb.velocity = fowardVelocity;
                    Debug.Log("RB " + rb.velocity);
                    break;
                }
            case TurnDirection.backward:
                {
                    rb.velocity = backwardVelocity;
                    break;
                }
        }
       
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(rb.velocity);
        if (nextTurningPoint != null && Mathf.Abs((transform.position - nextTurningPoint.position).magnitude) < distToTarget)
        {
            transform.position = new Vector3(nextTurningPoint.transform.position.x,transform.position.y, nextTurningPoint.transform.position.z);
            

            SetVelocity(turnDirection);
            nextTurningPoint = null;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void SetTurningPoint(Transform point, TurnDirection direction)
    {
        nextTurningPoint = point;
        turnDirection = direction;
    }

    public enum TurnDirection { left, right, foward, backward};
}
