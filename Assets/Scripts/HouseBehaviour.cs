using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour
{
    internal int houseType;
    internal bool delivered;
    bool onScale;
    float timer;

    void Start()
    {
        delivered = false;
        onScale = true;
        timer = 0;
    }

    void Update()
    {
        if (delivered)
        {
            timer += Time.deltaTime*2;
            if (timer >= 1)
            {
                onScale = false;
                timer = 1;
                if (houseType == 0)
                {
                    transform.localScale = new Vector3(0.1f, 1, 1);
                } 
                else
                {
                    transform.localScale = new Vector3(1, 1, 0.1f);
                }
            }
            if (onScale)
            {
                if (houseType == 0)
                {
                    transform.localScale = new Vector3(Elastic(timer)/ 10, Elastic(timer), Elastic(timer));
                }
                else
                {
                    transform.localScale = new Vector3(Elastic(timer), Elastic(timer), Elastic(timer) / 10);
                }
            }
        }
    }

    float Elastic(float x)
    {
        float c4 = (2 * Mathf.PI) / 3;
        return ((x == 0) ? 0 : ((x == 1) ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1));
    }
}
