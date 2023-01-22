using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] DisplayManager dsplmanager;
    [SerializeField] LevelManager lvlmanager;

    [SerializeField] internal AudioSource[] sndsDelivered;
    [SerializeField] internal AudioSource sndThrow;
    [SerializeField] internal AudioSource sndElastic;
    [SerializeField] internal AudioSource sndCarStart;
    [SerializeField] internal AudioSource sndApplause;
    [SerializeField] internal AudioSource sndSelect;

    [SerializeField] AudioSource sndAlldone;
    bool allPlayed;

    [SerializeField] internal AudioSource mscPostman;
    [SerializeField] internal AudioSource sndCitywave;

    void Start()
    {
        allPlayed = false;
        sndCarStart.pitch = 1 + (0.0316f * (LevelManager.level-1));
        mscPostman.pitch = 1 + (0.0316f * (LevelManager.level-1));
        mscPostman.Play();

    }

    void Update()
    {
        if (dsplmanager.lvlup && dsplmanager.timer <= 5.5f)
        {
            mscPostman.volume -= Time.deltaTime/4;
            sndCitywave.volume -= Time.deltaTime/4;
            if (mscPostman.volume <= 0.2)
            {
                mscPostman.volume = 0.2f;
                sndCitywave.volume = 0;
            }
        }

        if(LevelManager.level < 10)
        {
            if (dsplmanager.lvlup && dsplmanager.timer > 5.5f)
            {
                mscPostman.volume -= Time.deltaTime/4;
                sndCitywave.volume -= Time.deltaTime / 4;
                if (mscPostman.volume <= 0)
                {
                    mscPostman.volume = 0;
                    sndCitywave.volume = 0;
                }
            }
        }
        else
        {
            if (dsplmanager.lvlup && (dsplmanager.toMenu || dsplmanager.toRetry))
            {
                mscPostman.volume -= Time.deltaTime/4;
                sndCitywave.volume -= Time.deltaTime/4;

                if (mscPostman.volume <= 0)
                {
                    mscPostman.volume = 0;
                    sndCitywave.volume = 0;
                }
            }
        }

        if (dsplmanager.toMenu)
        {
            mscPostman.volume -= Time.deltaTime/4;
            sndCitywave.volume -= Time.deltaTime/4;

            if (mscPostman.volume <= 0)
            {
                mscPostman.volume = 0;
                sndCitywave.volume = 0;
            }
        }

        if ((LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count) == LevelManager.orderBook[LevelManager.level] && !allPlayed && dsplmanager.run)
        {
            sndAlldone.Play();
            allPlayed = true;
        }
    }
}
