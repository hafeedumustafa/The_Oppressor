using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour
{
    
    public Transform player;
    bool NotHighEnough;
    public float SeeingRange;

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;
        
        if(Physics.Raycast(transform.position, direction,out hit, SeeingRange)) {
            if(hit.collider.tag == "Player") {
            print("player in view");}
        }
    }
}
