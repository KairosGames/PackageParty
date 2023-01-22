using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMenuBehavior : MonoBehaviour
{
    float timer;

    void Start()
    {
        timer = 20.0f;
        transform.rotation = new Quaternion(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), transform.rotation.w);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
