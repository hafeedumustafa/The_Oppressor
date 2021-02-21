using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    float initialSpeed;
    float groundCheck;
    bool isGrounded = false;
    public GameObject Feet;

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
            print("slower");
            speed = initialSpeed - 2f;
        }
        else if(Input.GetKey(KeyCode.LeftShift)) {
            print("faster");
            speed = initialSpeed + 3.5f;
        }
        else {
            speed = initialSpeed;
        }


        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * HorizontalMovement + transform.forward * VerticalMovement;
        transform.position += move * Time.deltaTime * speed;

        //gravity
        int Ground = 1 << 9;
        Ground = ~Ground;

        isGrounded = Physics.Raycast(Feet.transform.position, transform.TransformDirection(Vector3.down), 0.1f, Ground);
        if(!isGrounded)
        {
            //
        }
    }
}
