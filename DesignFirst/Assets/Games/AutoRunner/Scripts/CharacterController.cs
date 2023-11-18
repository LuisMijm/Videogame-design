using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterController : MonoBehaviour
{

    // public GameObject Hero_;
    public float forwardSpeed_;
    public float lateralSpeed_;
    public float jumpForce_;
    public float FallingCastDistance_;
    public int coins_;
    public int distance_;
    public Camera mainCamera_;

    public ParticleSystem particleSystem_;

    public Animator anim_;
    private string currentState_;

    Vector3 spawmPoint_;
    public GameObject deathPlane_;

    public Rigidbody rb_;

    public GameObject floorObj_;
    public FloorController floorControl_;

    public bool isGrounded_;

    
    // Start is called before the first frame update
    void Start()
    {
        lateralSpeed_ = 15.0f;
        forwardSpeed_ = 20.0f;
        spawmPoint_ = new Vector3(-1.2f, 8.54f, -785.41f);

        // jumpForce_ = 5.0f;
        rb_ = GetComponent<Rigidbody>();
        FallingCastDistance_ = 1.0f;
        coins_ = 0;
        distance_ = 0;
        floorObj_ = GameObject.Find("FloorObj");
        floorControl_ = floorObj_.GetComponent<FloorController>();
        // anim_ = GetComponent<Animator>();

    }


    void Run()
    {
        // this.transform.Translate(Vector3.forward * forwardSpeed_ * Time.deltaTime);
        float verticalVelocity = rb_.velocity.y;
        float sideVelocity = rb_.velocity.x;
        Vector3 newVelocity;

        newVelocity = Vector3.forward * forwardSpeed_;
        newVelocity.x = sideVelocity;
        newVelocity.y = verticalVelocity;

        rb_.velocity = newVelocity;
        
        distance_++;
    }

    public void ResetPosition()
    {
        this.transform.position = spawmPoint_;
        // coins_ = 0;
        distance_ = 0;
        this.transform.rotation = Quaternion.Euler(0,0,0);
    }

    void ActivateParticles()
    {
        // if (particleSystem_.isPaused)
        // {
            Debug.Log("Reanudando partículas");
            particleSystem_.Play();
            Debug.Log("¿Partículas activas?: " + particleSystem_.isPlaying);

        // }
    }

    void DeactivateParticleSystem()
    {
        if (particleSystem_.isPlaying)
        {
            Debug.Log("Deteniendo partículas");
            particleSystem_.Stop();
            // particleSystem_.Clear();
            // particleSystem_.
            Debug.Log("¿Partículas detenidas?: " + particleSystem_.isPlaying);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState_ == newState) return;
        anim_.Play(newState);
        currentState_ = newState;
    }


    // Update is called once per frame
    void Update()
    {
        // Movimiento

        if(Input.GetKey(KeyCode.W))
        {
            // Debug.Log("W");
            //this.transform.Translate(Vector3.forward * speed_ * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Debug.Log("A");
            this.transform.Translate(Vector3.left * lateralSpeed_ * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.S))
        {
            // Debug.Log("S");
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
            // Debug.Log("Up Arrow");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Debug.Log("Left Arrow");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Debug.Log("Right Arrow");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Debug.Log("Down Arrow");
        }

        // General Controls


        if (Input.GetKey(KeyCode.Return))
        {
            // Debug.Log("Enter");
        }

        deathPlane_.transform.position = Vector3.Lerp(deathPlane_.transform.position,
                                                      new Vector3(this.transform.position.x,
                                                                  deathPlane_.transform.position.y,
                                                                  this.transform.position.z),
                                                      1.0f);


        // Debug.Log("ground: " + anim_.GetBool("ground"));
        if (Physics.Raycast(this.transform.position, Vector3.down, FallingCastDistance_, LayerMask.GetMask("Floor")))
        {
            // Debug.Log("TouchingGround" + true);
            // anim_.SetBool("ground", true);
            
            //anim_.SetBool("falling_", false);

            if (Input.GetKey(KeyCode.Space))
            {
                rb_.AddForce(Vector3.up * jumpForce_, ForceMode.Impulse);
                // anim_.SetBool("ground", false);
                // anim_.Play("Jump");
                ActivateParticles();
            }
            ChangeAnimationState("Run");
            isGrounded_ = true;
            mainCamera_.fieldOfView = Mathf.Lerp(mainCamera_.fieldOfView, 60, 0.01f);

            DeactivateParticleSystem();
        }
        else
        {
            // anim_.SetBool("falling_", true);
            // anim_.SetBool("ground", false);
            // Debug.Log("TouchingGround" + false);
            //mainCamera_.fieldOfView = 80;
            mainCamera_.fieldOfView = Mathf.Lerp(mainCamera_.fieldOfView, 80, 0.01f);
            ChangeAnimationState("Jump");
            isGrounded_ = false;
        }
        Debug.DrawRay(this.transform.position, Vector3.down * FallingCastDistance_, Color.red);

    }

    private void FixedUpdate()
    {
        Run();

        // Debug.Log("ground: " + anim_.GetBool("ground"));
        // if (Physics.Raycast(this.transform.position, Vector3.down, FallingCastDistance_, LayerMask.GetMask("Floor")))  
        // {
        //     // Debug.Log("TouchingGround" + true);
        //     anim_.SetBool("ground", true);

        //     //anim_.SetBool("falling_", false);

        //     if (Input.GetKey(KeyCode.Space))
        //     {
        //         rb_.AddForce(Vector3.up * jumpForce_, ForceMode.Impulse);
        //         anim_.SetBool("ground", false);
        //     }
        //     mainCamera_.fieldOfView = Mathf.Lerp(mainCamera_.fieldOfView, 60, 0.1f);

        //     DeactivateParticleSystem();
        // }  
        // else
        // {
        //     //anim_.SetBool("falling_", true);
        //     // anim_.SetBool("ground", false);
        //     // Debug.Log("TouchingGround" + false);
        //     //mainCamera_.fieldOfView = 80;
        //     mainCamera_.fieldOfView = Mathf.Lerp(mainCamera_.fieldOfView, 80, 0.1f);

        //     ActivateParticles();
        // }
        // Debug.DrawRay(this.transform.position, Vector3.down * FallingCastDistance_, Color.red);

        // distance_++;

    }

    void OnTriggerEnter(Collider other)
    {
        if("Coin" == other.gameObject.tag)
        {
            coins_++;
            other.gameObject.SetActive(false);
        }
        // Debug.Log(other.gameObject.tag);

        if("FloorSpawner" == other.gameObject.tag)
        {
            Debug.Log("floors");
            floorControl_.newGeneration();
            floorControl_.generating_ = true;

            Collider otherCollider_;
            if ((otherCollider_ = other.gameObject.GetComponent<Collider>()) != null)
            {
                otherCollider_.enabled = false;
            }
        }

        if("Obstacle" == other.gameObject.tag)
        {
            ResetPosition();
            // floorControl_.ResetFloors();
        }
    }
}
