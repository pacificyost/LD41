using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;
    public float distToTarget = 0.5f;
    public TurnDirection currentDirection;
    private Transform nextTurningPoint;
    private TurnDirection turnDirection;
    private Rigidbody rb;

    private Dictionary<TurnDirection, Quaternion> rotations;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        InitializeRotations();
        SetVelocity(TurnDirection.right);
    }

    private void InitializeRotations()
    {
        rotations = new Dictionary<TurnDirection, Quaternion>();
        Quaternion right = new Quaternion();
        right.eulerAngles = new Vector3(0, 0, 0);
        Quaternion left = new Quaternion();
        left.eulerAngles = new Vector3(0, 180, 0);
        Quaternion foward = new Quaternion();
        foward.eulerAngles = new Vector3(0, 270, 0);
        Quaternion backward = new Quaternion();
        backward.eulerAngles = new Vector3(0, 90, 0);

        rotations[TurnDirection.right] = right;
        rotations[TurnDirection.left] = left;
        rotations[TurnDirection.foward] = foward;
        rotations[TurnDirection.backward] = backward;
        

       
    }

    private void SetVelocity(TurnDirection direction)
    {
        switch (direction)
        {
            case TurnDirection.right:
            case TurnDirection.left:
                {
                    rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    break;
                }
            case TurnDirection.foward:
            case TurnDirection.backward:
                {
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                    break;
                }            
        }
        transform.rotation = rotations[direction];
        rb.velocity = transform.right * speed;
        currentDirection = direction;
        turnDirection = direction;
       
    }
	
	// Update is called once per frame
	void Update () {

        if (nextTurningPoint != null && Mathf.Abs((transform.position - nextTurningPoint.position).magnitude) < distToTarget)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (nextTurningPoint != null)
        {
            transform.position = new Vector3(nextTurningPoint.transform.position.x, transform.position.y, nextTurningPoint.transform.position.z);
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
