using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public float endposition_;
    public float speed_;

    // Update is called once per frame
    private void Update()
    {
        if(transform.position.y > endposition_)
        // if(true)
        {
            transform.Translate((Vector3.up * -1.0f) * speed_ * Time.deltaTime);
        }
        else
        {
            this.enabled = false;
        }
    }
}
