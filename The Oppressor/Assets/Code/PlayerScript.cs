using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public GameObject HealthBar;

    public float speed = 5f;
    float initialSpeed;

    bool isGrounded = false;
    public GameObject Feet;
    float groundCheck;

    Vector3 velocity;

    public bool AllowGravity = true;
    public const float gravity = -21.81f;

    float jumpForce = 5f;

    public CharacterController CC;


    void Start()
    {
        initialSpeed = speed;
    }


    void Update()
    {
        // movement
        if(Input.GetKey(KeyCode.LeftControl)) 
            speed = initialSpeed - 2f;
        else if(Input.GetKey(KeyCode.LeftShift)) 
            speed = initialSpeed + 3.5f;
        else 
            speed = initialSpeed;
        


        float HorizontalMovement = Input.GetAxisRaw("Horizontal");
        float VerticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * HorizontalMovement + transform.forward * VerticalMovement;
        CC.Move(move * Time.deltaTime * speed);

        //jump

        if(Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -1 * gravity);

        //gravity
        int Ground = 1 << 9;

        isGrounded = Physics.CheckSphere(Feet.transform.position, 0.1f, Ground);
        if(isGrounded && AllowGravity && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if(AllowGravity && !isGrounded) {
        velocity.y += gravity * Time.deltaTime;}
        transform.position += velocity * Time.deltaTime;

    }

    void death() {
        print("*dies*");
    }

    public void Damaged(float damage) {
        float beforeHealth = PlayerManager.instance.health;
        PlayerManager.instance.health -= damage;
        if(PlayerManager.instance.health > 0) {
            HealthBar.GetComponent<healthBar>().HealthValueColor(beforeHealth);
            HealthBar.GetComponent<healthBar>().HealthValueTransition(beforeHealth, PlayerManager.instance.health);    }
        else {
            death();
            HealthBar.GetComponent<healthBar>().HealthValueTransition(beforeHealth, 0);    }
    }
}
