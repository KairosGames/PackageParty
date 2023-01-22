using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] DisplayManager dsplmanager;
    [SerializeField] Transform goTo;
    [SerializeField] Transform spawn;
    [SerializeField] Transform focus;
    [SerializeField] float speed = 10.0f;
    Vector3 dirToGo = new Vector3();
    Vector3 dirToGo2 = new Vector3();


    void Start()
    {
        dirToGo = (goTo.localPosition - transform.localPosition).normalized;
        dirToGo2 = (spawn.localPosition - focus.localPosition).normalized;
    }

    void Update()
    {
        if (dsplmanager.lvlup)
        {
            if ((goTo.localPosition-transform.localPosition).magnitude > 1.0f)
            {
                transform.localPosition += dirToGo * speed * Time.deltaTime;
                transform.LookAt(focus.position);
            }
            else
            {
                dsplmanager.transition = true;
            }

            if((spawn.localPosition - focus.localPosition).magnitude > 0.1f)
            {
                focus.localPosition += dirToGo2 * speed/4 * Time.deltaTime;
            }
        }
    }
}
