using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
    CharacterController cc_;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Player")
        {
            cc_ = other.gameObject.GetComponent<CharacterController>();
            cc_.ResetPosition();
        }
    }
}
