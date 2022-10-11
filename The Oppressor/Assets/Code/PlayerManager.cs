using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager instance;

    public float health;

    
    void Awake() 
    {
        if(instance == null) {
            instance = this;
        }
        health = SaveManager.instance.activeSave.health;
    }

}
