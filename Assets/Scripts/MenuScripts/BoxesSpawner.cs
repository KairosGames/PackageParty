using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesSpawner : MonoBehaviour
{
    [SerializeField] GameObject boxMenu;

    float timer;

    void Start()
    {
        timer = ((float)Random.Range(20, 100))/100.0f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = ((float)Random.Range(20, 100)) / 100.0f;
            var box = Instantiate(boxMenu);
            box.transform.position = transform.position;
        }
    }
}
