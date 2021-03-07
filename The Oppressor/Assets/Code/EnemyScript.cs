using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    
    public Transform player;

    public float speed;
    public float SeeingRange;
    bool inPlayerRange = false;

    public float health = 100;
    
    bool isGrounded = false;
    public GameObject Feet;
    Vector3 velocity;
    public const float gravity = -9.81f;
    public bool AllowGravity = true;

    void Update()
    {
        gravityFx();
        pathfinding();

    }

    void gravityFx()
    {
        int Ground = 1 << 9;

        isGrounded = Physics.CheckSphere(Feet.transform.position, 0.4f, Ground);
        if(isGrounded && AllowGravity && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if(AllowGravity && !isGrounded) {
        velocity.y -= gravity * Time.deltaTime;}
        transform.position += velocity * Time.deltaTime;

    }
    
    void pathfinding() {
        //RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;

        Vector3 differencePlayerToEnemy = 
        new Vector3(transform.position.x, player.position.y, transform.position.z) - 
        new Vector3(player.position.x, player.position.y, player.position.z);

        float distance = Mathf.Sqrt(Mathf.Pow(differencePlayerToEnemy.x, 2) + Mathf.Pow(differencePlayerToEnemy.z, 2));

        int PlayerMask = 1 << 10;
        

        inPlayerRange = Physics.CheckCapsule(
            new Vector3(transform.position.x, player.position.y + 1, transform.position.z), 
            new Vector3(transform.position.x, player.position.y - 1, transform.position.z), 4, PlayerMask);
        

        //for later use (after A* algorithm ): && Physics.Raycast(transform.position, direction,out hit, SeeingRange)
        if(!inPlayerRange && distance < 25) {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    public void Hit(float damage) {
        health -= damage;
        if(health <= 0) {
            death();
        }
    }

    void death()
    {

    }
}
