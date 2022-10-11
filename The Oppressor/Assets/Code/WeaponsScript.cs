using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsScript : MonoBehaviour
{

    public GameObject[] weapons;
    
    void Start()
    {
        for(int i = 0; i < SaveManager.instance.activeSave.maxWeaponSlot; i++) {
            weapons[i] = SaveManager.instance.activeSave.weapons[i];
            GameObject weapon = Instantiate(SaveManager.instance.activeSave.weapons[i]);
        }

    }
}
