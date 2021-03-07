using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour
{
    
    public Transform player;

    public float speed;
    bool NotHighEnough;
    public float SeeingRange;
    bool inPlayerRange = false;

    void Update()
    {
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
}
