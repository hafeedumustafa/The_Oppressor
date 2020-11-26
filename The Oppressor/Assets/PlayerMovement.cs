using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController CC;
    public float MaincameraEulerAngleY;
    public float speed = 5f;
    
    void Start()
    {
    }

    void Update()
    {
        movement();
    }

    void movement()
    {
        //Input
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Direction (without camera angle influence)
        Vector3 InitialDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        if(InitialDirection.magnitude >= 0.1f) { //check if moving
            float SmoothDampAngle = 0f; //initialize SDA

            float HeadingAngle = Mathf.Atan2(InitialDirection.x, InitialDirection.z) * Mathf.Rad2Deg + MaincameraEulerAngleY; //Direction pointing w/ camera angle
            float CurrentAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, HeadingAngle, ref SmoothDampAngle, 0.1f); //Transition from old-new position
            transform.rotation = Quaternion.Euler(0f, CurrentAngle, 0f); // rotate using transition

            Vector3 WorldSpaceDirection = Quaternion.Euler(0f, HeadingAngle, 0f) * Vector3.forward; // using direction w/ cam angle to complete direction formula
            CC.Move(WorldSpaceDirection.normalized * speed * Time.deltaTime); // moving to direction

        }
    }

}
