using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPointController : MonoBehaviour {

    public PlayerController.TurnDirection turnDirectionX;
    public PlayerController.TurnDirection turnDirectionZ;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();

        if (pc.currentDirection == PlayerController.TurnDirection.left || pc.currentDirection == PlayerController.TurnDirection.right)
        {
            pc.SetTurningPoint(transform, turnDirectionZ);
        }
        else
        {
            pc.SetTurningPoint(transform, turnDirectionX);
        }
    }
}
