using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    // public GameObject Hero_;
    public float forwardSpeed_;
    public float lateralSpeed_;
    public float jumpForce_;
    public float FallingCastDistance_;

    //public Animator anim_;

    Vector3 spawmPoint_;
    public GameObject deathPlane_;

    public Rigidbody rb_;
    
    // Start is called before the first frame update
    void Start()
    {
        lateralSpeed_ = 15.0f;
        forwardSpeed_ = 20.0f;
        spawmPoint_ = new Vector3(-1.2f, 8.54f, -806.29f);
        jumpForce_ = 5.0f;
        rb_ = GetComponent<Rigidbody>();
        FallingCastDistance_ = 1.0f;
    }


    void Run()
    {
        this.transform.Translate(Vector3.forward * forwardSpeed_ * Time.deltaTime);
    }

    public void ResetPosition()
    {
        this.transform.position = spawmPoint_;
    }


    // Update is called once per frame
    void Update()
    {

        Run();

        // Movimiento

        if(Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            //this.transform.Translate(Vector3.forward * speed_ * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Debug.Log("A");
            this.transform.Translate(Vector3.left * lateralSpeed_ * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S");
            //this.transform.Translate(Vector3.back * speed_ * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // Debug.Log("D");
            this.transform.Translate(Vector3.right * lateralSpeed_ * Time.deltaTime);
        }

        // Arrows

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Up Arrow");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Left Arrow");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Down Arrow");
        }

        // General Controls


        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Enter");
        }

        deathPlane_.transform.position = Vector3.Lerp(deathPlane_.transform.position,
                                                      new Vector3(this.transform.position.x,
                                                                  deathPlane_.transform.position.y,
                                                                  this.transform.position.z),
                                                      1.0f);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, Vector3.down, FallingCastDistance_, LayerMask.GetMask("Floor")))  
        {
            Debug.Log("TouchingGround" + true);
        
            //anim_.SetBool("falling_", false);

            if (Input.GetKey(KeyCode.Space))
            {
                rb_.AddForce(Vector3.up * jumpForce_, ForceMode.Impulse);
            }
        }
        else
        {
            //anim_.SetBool("falling_", true);

            Debug.Log("TouchingGround" + false);
        }
        Debug.DrawRay(this.transform.position, Vector3.down * FallingCastDistance_, Color.red);
    }
}
