using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f;
    public float distToTarget = 0.8f;
    public TurnDirection currentDirection;
    private Transform nextTurningPoint;
    private TurnDirection turnDirection;
    private Rigidbody rb;
    public float jumpSpeed = 1.0f;
    private float jumpScale = 100.0f;
    private float groundedDist = 0.2f;
    private float horizontalDist = 0.5f;
    private List<Ray> horizontalRays;
    private List<Ray> verticalRays;

    private Dictionary<TurnDirection, Quaternion> rotations;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        InitializeRotations();
        SetVelocity(TurnDirection.right);
        InitializeRays();
    }

    private void InitializeRays()
    {
        horizontalRays = new List<Ray>();
        verticalRays = new List<Ray>();

        horizontalRays.Add(new Ray(new Vector3(0, 0.1f, 0), Vector3.right));
        horizontalRays.Add(new Ray(new Vector3(0, 1.0f, 0), Vector3.right));

        verticalRays.Add(new Ray(new Vector3(0, 0.1f, 0), Vector3.up * -1));
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
        
        //MoveRight();
        currentDirection = direction;
        turnDirection = direction;
       
    }
	
	// Update is called once per frame
	void Update () {

        if (nextTurningPoint != null && Mathf.Abs((transform.position.x - nextTurningPoint.position.x)) < distToTarget && Mathf.Abs((transform.position.z - nextTurningPoint.position.z)) < distToTarget)
        {
            Turn();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        
        MoveRight();
        
    }

    private bool CheckForPlatforms(Ray ray, float distance)
    {
        RaycastHit hit;
        ray.origin += transform.position;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.gameObject.tag == "Platform")
            {
                return true;
            }
        }
        return false;
    }

    private void MoveRight()
    {
        bool platformFound = false;
        foreach (Ray ray in horizontalRays)
        {
            if (CheckForPlatforms(ray, horizontalDist) == true)
            {
                platformFound = true;
                break;
            }
        }

        if (platformFound == false)
        {
            transform.position = transform.position + (transform.right * speed * Time.deltaTime);
        }
    }



    private void Jump()
    {
        bool platformFound = false;
        foreach (Ray ray in verticalRays)
        {
            if (CheckForPlatforms(ray, groundedDist) == true)
            {
                platformFound = true;
                break;
            }
        }

        if (platformFound == true)
        {
            rb.AddForce(transform.up * jumpSpeed * jumpScale);
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

    public void SetTurningPoint(Transform point, TurnDirection direction)
    {
        nextTurningPoint = point;
        turnDirection = direction;
    }

    public enum TurnDirection { left, right, foward, backward};
}
