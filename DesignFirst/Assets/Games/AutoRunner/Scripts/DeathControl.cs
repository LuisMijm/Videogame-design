using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
    CharacterController cc_;

    public GameObject floorObj_;
    public FloorController floorControl_;

    void Start()
    {
        floorObj_ = GameObject.Find("FloorObj");
        floorControl_ = floorObj_.GetComponent<FloorController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            cc_ = other.gameObject.GetComponent<CharacterController>();
            cc_.ResetPosition();
            floorControl_.ResetFloors();
        }
    }
}
