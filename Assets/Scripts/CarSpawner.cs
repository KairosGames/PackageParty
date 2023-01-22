using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] AudioSource sndCarHorn;

    internal Transform spawn;

    [SerializeField] GameObject[] vehicles;
    static internal int index = 0;
    GameObject vehicle;

    bool active;
    bool horn;

    void Start()
    {
        vehicle = vehicles[index];
        vehicle.SetActive(true);
        active = false;
        horn = false;
        index++;
    }

    void Update()
    {
        if((spawn.position-transform.position).magnitude < 200)
        {
            active = true;
        }
        if((spawn.position-vehicle.transform.position).magnitude < 80 && !horn)
        {
            sndCarHorn.Play();
            horn = true;
        }
        if (active)
        {
            vehicle.transform.Translate(Vector3.left * Time.deltaTime * TruckSpeed.speed);
        }
        if((spawn.position - transform.position).magnitude > 220 && active)
        {
            gameObject.SetActive(false);
        }
    }
}
