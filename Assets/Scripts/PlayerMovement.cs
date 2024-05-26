using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private CharacterController controller;

    // public float speed = 12f;
    // public float gravity = -9.81f * 2f;
    // public float jumpHeight = 3f;

    // public Transform groundCheck;
    // public float groundDistance = 0.4f;
    // public LayerMask groundMask;

    // bool isGrounded;
    // bool isMoving;

    // Vector3 velocity;
    // Vector3 lastPosition = new Vector3(0, 0, 0);

    // private void Start()
    // {
    //     controller = GetComponent<CharacterController>();
    // }

    // private void Update()
    // {
    //     isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    //     //Resetting the velocity

    //     if (isGrounded && velocity.y < 0)
    //     {
    //         velocity.y = -2f;
    //     }

    //     //Getting Input
    //     float x = Input.GetAxis("Horizontal");
    //     float z = Input.GetAxis("Vertical");

    //     Vector3 move = transform.right * x + transform.forward * z;

    //     // Player actually moving
    //     controller.Move(move * speed * Time.deltaTime);

    //     // Check if the Player can Jump
    //     if (isGrounded && Input.GetButtonDown("Jump"))
    //     {
    //         velocity.y = Mathf.Sqrt(gravity * -2f * jumpHeight);
    //     }

    //     //Falling down
    //     velocity.y += gravity * Time.deltaTime;

    //     //Exuting the jump
    //     controller.Move(velocity * Time.deltaTime);

    //     // if Player moving
    //     if (lastPosition != gameObject.transform.position && isGrounded)
    //     {
    //         isMoving = true;
    //     }
    //     else
    //     {
    //         isMoving = false;
    //     }

    //     lastPosition = gameObject.transform.position;

    // }

    private CharacterController controller;

    public float speed = 500f;
    public float gravity = -9.81f * 2f;
    public float jumpHeight = 3f;

    public bool isGrounded;
    public bool isMoving;



    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    Vector3 lastPosition = new Vector3(0, 0, 0);


    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Getting Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Player Actually moving
        controller.Move(move * speed * Time.deltaTime);

        // Player Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        // Falling Down
        velocity.y += gravity * Time.deltaTime;

        // Executing Jump
        controller.Move(velocity * Time.deltaTime);

        // Player Moving
        if (isGrounded && lastPosition != gameObject.transform.position)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }



    }
}

