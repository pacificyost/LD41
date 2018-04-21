using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPointController : MonoBehaviour {

    public PlayerController.TurnDirection turnDirection;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();

        pc.SetTurningPoint(transform, turnDirection);
        Debug.Log("trigger entered");
    }
}
