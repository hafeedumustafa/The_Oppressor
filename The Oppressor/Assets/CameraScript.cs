using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerMovement playerMovement;

    void Update()
    {
        playerMovement.MaincameraEulerAngleY = this.gameObject.transform.eulerAngles.y;
    }
}
