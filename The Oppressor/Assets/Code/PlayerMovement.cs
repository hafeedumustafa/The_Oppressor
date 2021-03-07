using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    float initialSpeed;

    bool isGrounded = false;
    public GameObject Feet;
    float groundCheck;

    Vector3 velocity;

    public bool AllowGravity = true;
    public const float gravity = -9.81f;

    float jumpForce = 5f;

    public CharacterController CC;


    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        if(Input.GetKey(KeyCode.LeftControl)) {
            speed = initialSpeed - 2f;
        }
        else if(Input.GetKey(KeyCode.LeftShift)) {
            speed = initialSpeed + 3.5f;
        }
        else {
            speed = initialSpeed;
        }


        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * HorizontalMovement + transform.forward * VerticalMovement;
        CC.Move(move * Time.deltaTime * speed);

        //jump

        if(Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -1 * gravity);

        //gravity
        int Ground = 1 << 9;

        isGrounded = Physics.CheckSphere(Feet.transform.position, 0.4f, Ground);
        if(isGrounded && AllowGravity && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if(AllowGravity && !isGrounded) {
        velocity.y += gravity * Time.deltaTime;}
        transform.position += velocity * Time.deltaTime;

    }
}
