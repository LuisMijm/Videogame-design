using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    float rotationSpeed_ = 90.0f * 2;
    private Transform tr_; 

    void Start()
    {
        // transform.LookAt(0.0f, 0.0f, 90.0f);
        // transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        // Quaternion.Euler(0.0f, 0.0f, 90.0f)

        tr_ = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetParent(null);
        // transform.Rotate(0, rotationSpeed_ * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0.0f, rotationSpeed_ * Time.deltaTime, 0.0f), Space.World);
        // transform.localRotation = Quaternion.Euler(new Vector3(0, 45, 0));
        // transform.Rotate(Vector3.forward, rotationSpeed_ * Time.deltaTime);
        transform.SetParent(tr_);
    }
}
