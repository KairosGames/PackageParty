using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpeed : MonoBehaviour
{
    [SerializeField] DisplayManager dsplmanager;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LevelManager lvlmanager;
    [SerializeField] PlayerControler plyrcontroler;
    [SerializeField] GameObject nitro;
    [SerializeField] AudioManager audiomanager;

    internal static float speed = 10.0f;

    bool nitroOn;
    bool stop;

    private void Start()
    {
        speed = 10.0f + ((LevelManager.level - 1) * 4);
        nitroOn = false;
        stop = false;
    }

    void Update()
    {
        if (dsplmanager.run && !stop)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if ((LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count) == LevelManager.orderBook[LevelManager.level])
            { 
                transform.Translate(Vector3.right * Time.deltaTime * 20);
                if (!nitroOn) { nitro.SetActive(true); }
                nitroOn = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "lvlup")
        {
            dsplmanager.lvlup = true;
            audiomanager.sndApplause.Play();
            lineRenderer.enabled = false;
            var points = (lvlmanager.housesToDeliver.Count == LevelManager.orderBook[LevelManager.level]) ? 0 : (1000 - ((1000 / LevelManager.orderBook[LevelManager.level]) * lvlmanager.housesToDeliver.Count));
            var bonus = (plyrcontroler.nbThrows != 0) ? ((500 / 100) * (((LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count) * 100) / plyrcontroler.nbThrows)) : 0;
            LevelManager.score += points + bonus;
            if (LevelManager.level == 10)
                if (LevelManager.highscore <= LevelManager.score)
                    LevelManager.highscore = LevelManager.score;
        }

        if (other.tag == "Finish")
        {
            stop = true;
        }
    }
}
