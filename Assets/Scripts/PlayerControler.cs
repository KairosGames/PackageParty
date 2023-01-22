using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] LevelManager lvlmanager;
    [SerializeField] DisplayManager dsplmanager;
    [SerializeField] AudioManager audiomanager;

    [SerializeField] Transform posCam;
    [SerializeField] Animator animator;
    float animTimer;
    bool animSwitch;
    internal int nbThrows;

    [SerializeField] GameObject spawn;
    [SerializeField] GameObject box;

    Vector2 touchIn = new Vector2();
    Vector2 touchOut = new Vector2();

    List<float> launcher = new List<float>();
    List<Vector3> launcherDir = new List<Vector3>();

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animTimer = 0.2f;
        animSwitch = false;
        nbThrows = 0;
    }

    void Update()
    {
        if (dsplmanager.run && !dsplmanager.lvlup)
        {
            /*if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchIn = touch.position;
                        spawn.GetComponent<LineProjectile>().onAim = true;
                        spawn.GetComponent<LineRenderer>().enabled = true;
                        audiomanager.sndElastic.Play();
                        break;
                    case TouchPhase.Moved:
                        touchOut = touch.position;
                        var lcldir = touchIn - touchOut;
                        spawn.GetComponent<LineProjectile>().aimDirection = lcldir;
                        break;
                    case TouchPhase.Ended:
                        spawn.GetComponent<LineProjectile>().onAim = false;
                        spawn.GetComponent<LineRenderer>().enabled = false;
                        touchOut = touch.position;
                        var dir = touchIn - touchOut;
                        if (!animSwitch)
                        {
                            launcher.Add(0.1f);
                            launcherDir.Add(dir);
                        }
                        animSwitch = true;
                        animator.SetBool("throw", true);
                        break;
                }
            }*/

            if (Input.GetButtonDown("Fire1"))
            {
                touchIn = Input.mousePosition;
                spawn.GetComponent<LineProjectile>().onAim = true;
                spawn.GetComponent<LineRenderer>().enabled = true;
                audiomanager.sndElastic.Play();
            }
            if (Input.GetButton("Fire1"))
            {
                touchOut = Input.mousePosition;
                var lcldir = touchIn - touchOut;
                spawn.GetComponent<LineProjectile>().aimDirection = lcldir;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                spawn.GetComponent<LineProjectile>().onAim = false;
                spawn.GetComponent<LineRenderer>().enabled = false;
                touchOut = Input.mousePosition;
                var dir = touchIn - touchOut;
                if (!animSwitch)
                {
                    launcher.Add(0.1f);
                    launcherDir.Add(dir);
                }
                animSwitch = true;
                animator.SetBool("throw", true);
            }
        }
        for (int i = launcher.Count-1; i>=0; i--)
        {
            launcher[i] -= Time.deltaTime;
            if (launcher[i] <= 0)
            {
                nbThrows++;
                var lBox = Instantiate(box);
                var script = lBox.GetComponent<BoxBehaviour>();
                lBox.transform.position = spawn.transform.position;
                script.direction = launcherDir[i];
                script.posCam = posCam;
                script.lvlmanager = lvlmanager;
                script.audiomanager = audiomanager;
                launcher.RemoveAt(i);
                launcherDir.RemoveAt(i);
                audiomanager.sndThrow.Play();
            }
        }
        if (animSwitch)
        {
            animTimer -= Time.deltaTime;
            if(animTimer<= 0)
            { 
                animSwitch = false;
                animator.SetBool("throw", false);
                animTimer = 0.2f;
                transform.localRotation = new Quaternion(transform.localRotation.x, -0.8f, transform.localRotation.z, transform.localRotation.w);
                transform.localPosition = new Vector3(-3.7f,3.8f,-4.3f);
            }
        }
    }
}
