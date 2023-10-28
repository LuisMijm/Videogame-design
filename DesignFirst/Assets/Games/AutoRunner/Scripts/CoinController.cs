using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    float rotationSpeed_ = 90.0f * 2;

    void Start()
    {
        transform.LookAt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed_ * Time.deltaTime);
        // transform.Rotate(Vector3.forward, rotationSpeed_ * Time.deltaTime);
    }
}
