using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    
    public Transform player;
    public CharacterController CC;
    public float health = 100;

    public float speed;
    [HideInInspector]public bool inPlayerRange = false;
    
    public GameObject Feet;
    Vector3 velocity;
    [HideInInspector]public bool isGrounded = false;
    public const float gravity = -9.81f;
    public bool AllowGravity = true;
    public bool AllowMovement = true;
    public float distance;// distance from player to enemy

    public float maxDistance = 25;
    

    void Update()
    {
        if(AllowGravity) 
            gravityFx();
        
        if(AllowMovement)
            pathfinding();

    }

    void gravityFx()
    {
        int Ground = 1 << 9;
        isGrounded = Physics.CheckSphere(Feet.transform.position, 0.01f, Ground);

        if(isGrounded && velocity.y < 0)
            velocity.y = 0f;

        if(!isGrounded) {
            velocity.y += gravity * Time.deltaTime;}
        CC.Move(velocity * Time.deltaTime);

    }
    
    void pathfinding() {
        Vector3 difference = player.position - transform.position;

        Vector2 differencePlayerToEnemy = 
        new Vector2(transform.position.x, transform.position.z) - 
        new Vector2(player.position.x, player.position.z);

        //distance = Mathf.Sqrt(Mathf.Pow(differencePlayerToEnemy.x, 2) + Mathf.Pow(differencePlayerToEnemy.y, 2));
        distance = (new Vector2(transform.position.x, transform.position.z) - new Vector2(player.position.x, player.position.z)).sqrMagnitude;
        // ^--- left off

        int PlayerMask = 1 << 10;
        

        inPlayerRange = Physics.CheckCapsule(
            new Vector3(transform.position.x, player.position.y + 1, transform.position.z), 
            new Vector3(transform.position.x, player.position.y - 1, transform.position.z), 4, PlayerMask);
        

        if(!inPlayerRange && distance < maxDistance * maxDistance) {
            float angle = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg + 90;
            transform.eulerAngles = Vector3.down * (angle - 180);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            CC.Move(forward * Time.deltaTime * speed);
        }
    }
    // Enemy Damaged
    public void Attacked(float damage) {
        health -= damage;
        if(health <= 0) {
            death();
        }
    }
    // Enemy Death
    void death()
    {
        print("*dies*");
    }
    // Enemy Hits Player
    private Vector2 DamageRange;

    public void HitPlayerA(float RDA) { // RDA == Random Damage A (A is for first, b is for second)
        DamageRange = new Vector2(RDA, DamageRange.y);
    }
    public void HitPlayerB(float RDB) {
        DamageRange = new Vector2(DamageRange.x, RDB);// RDA == Random Damage B (A is for first, b is for second)
        HitPlayer(DamageRange);
    }
    public void HitPlayer(Vector2 DamageRange) 
    {
        float damage = Random.Range(DamageRange.x, DamageRange.y);
        player.gameObject.GetComponent<PlayerScript>().Damaged(damage);
    }
}
