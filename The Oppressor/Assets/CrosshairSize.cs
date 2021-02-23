using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairSize : MonoBehaviour
{
    
    RectTransform rt;
    [Range(60, 200)]
    public float size;
    [Range(60, 200)]
    public float defaultSize;

    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(defaultSize, defaultSize);
        print(rt.sizeDelta);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            float sizeto = Random.Range(80, 150);
            IncreaseSize(sizeto);
        print(sizeto);
        }
    }

    public void IncreaseSize(float sizeto)
    {
        if(rt == null) {
            rt = gameObject.GetComponent<RectTransform>();
        }
        print(sizeto + "ok");

        StartCoroutine(Sizing(rt.sizeDelta.x, sizeto, 5f));
    }

    IEnumerator Sizing(float startSize, float endsize, float speed) { // smaller speed is the better it looks
        float rtSize = rt.sizeDelta.x;
        if((rtSize < endsize && startSize < endsize) || (rtSize > endsize && startSize > endsize)) {
            float incrementValue = (startSize - endsize) / speed;
            rt.sizeDelta -= new Vector2(incrementValue, incrementValue);
            size = rtSize;
            
            yield return new WaitForFixedUpdate();
            StartCoroutine(Sizing(startSize, endsize, speed));
        }
        else {
            print("ab");

        }
    }
}
