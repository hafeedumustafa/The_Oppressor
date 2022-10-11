using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Color Green;
    public Color Red;
    public Color HealthBarColour;

    public Slider HealthBar;
    public Image HealthColour;

    public PlayerScript Player;
    
    void Start() {
        HealthBarColour.r = Mathf.Lerp(Red.r, Green.r, PlayerManager.instance.health / 100);
        HealthBarColour.g = Mathf.Lerp(Red.g, Green.g, PlayerManager.instance.health / 100);
        HealthBarColour.b = Mathf.Lerp(Red.b, Green.b, PlayerManager.instance.health / 100);

        HealthColour.color = HealthBarColour;
        
    }

    public void HealthValueColor(float beforeHealth) {
        StartCoroutine(HVC(0.0f, beforeHealth));

    }

    IEnumerator HVC(float time, float beforeHealth) {
        time += 0.1f;
        HealthBarColour.r = Mathf.Lerp(Mathf.Lerp(Red.r, Green.r, beforeHealth / 100), Mathf.Lerp(Red.r, Green.r, PlayerManager.instance.health / 100), time);
        HealthBarColour.g = Mathf.Lerp(Mathf.Lerp(Red.g, Green.g, beforeHealth / 100), Mathf.Lerp(Red.g, Green.g, PlayerManager.instance.health / 100), time);
        HealthBarColour.b = Mathf.Lerp(Mathf.Lerp(Red.b, Green.b, beforeHealth / 100), Mathf.Lerp(Red.b, Green.b, PlayerManager.instance.health / 100), time);
        HealthColour.color = HealthBarColour;
        yield return new WaitForFixedUpdate();
        if(time < 1) 
            StartCoroutine(HVC(time, beforeHealth));
        else
            print("HVCOLOUR DONE!");
    }

    public void HealthValueTransition(float currentValue, float nextValue) {
        StartCoroutine(HVT(currentValue, nextValue, 0f));
    }

    IEnumerator HVT(float currentValue, float nextValue, float time) {
        time += 0.1f;
        HealthBar.value = Mathf.Lerp(currentValue, nextValue, time);
        yield return new WaitForFixedUpdate();
        
        if(HealthBar.value > nextValue)
            StartCoroutine(HVT(currentValue, nextValue, time));
        else
            print("HVTRANSITION DONE");
    }
}
