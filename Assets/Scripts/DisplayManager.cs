using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] LevelManager lvlmanager;
    [SerializeField] AudioManager audiomanager;

    internal bool run, lvlup, transition, started, toMenu, toRetry;
    bool startedPlayed;
    internal float timer;
    [SerializeField] TruckSpeed truck;
    [SerializeField] PlayerControler plyrcontroler;

    [SerializeField] Image[] curtains;
    [SerializeField] Image bckground;
    [SerializeField] Image banner;

    [SerializeField] Text level;
    [SerializeField] Text deliveries;
    [SerializeField] Text points;
    [SerializeField] Text accuracy;
    [SerializeField] Text bonus;
    [SerializeField] Text score;
    [SerializeField] Text highscore;

    [SerializeField] Button startButton;
    [SerializeField] Button menuButton;
    [SerializeField] Button retryButton;

    [SerializeField] Text delivIG;
    [SerializeField] Image house;

    void Start()
    {
        run = false;
        lvlup = false;
        transition = true;
        started = false;
        toMenu = false;
        toRetry = false;
        timer = 0;
        startedPlayed = false;
        curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        bckground.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        level.text = $"Level {LevelManager.level}";
        deliveries.text = $"Orders : {LevelManager.orderBook[LevelManager.level]}";
        score.text = $"Score : {LevelManager.score}";
    }

    void Update()
    {
        if (transition || started)
        {
            timer += Time.deltaTime;

        }
        if (timer >= 0.5 && transition && !lvlup && !started)
        {
            curtains[0].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 1200);
            curtains[1].rectTransform.sizeDelta -= new Vector2(0, Time.deltaTime * 1200);

            if (curtains[0].rectTransform.sizeDelta.y <= 1 || curtains[1].rectTransform.sizeDelta.y <= 1)
            {
                curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, 0);
                curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, 0);
                transition = false;
                timer = 0;
            }
        }

        if(timer>= 0.1 && started && !startedPlayed)
        {
            audiomanager.sndCarStart.Play();
            startedPlayed = true;
        }

        if(timer >=0.5 && started)
        {
            run = true;
            started = false;
            timer = 0;
        }

        if(transition && lvlup)
        {
            if (timer >= 0.5f)
            {
                if (!toMenu && !toRetry)
                {
                    bckground.gameObject.SetActive(true);
                    banner.gameObject.SetActive(true);
                    level.gameObject.SetActive(true);
                    deliveries.text = $"Deliveries : {LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count}/{LevelManager.orderBook[LevelManager.level]}";
                    deliveries.gameObject.SetActive(true);
                    points.text = $"+ {((lvlmanager.housesToDeliver.Count == LevelManager.orderBook[LevelManager.level]) ? 0 : (1000 - ((1000 / LevelManager.orderBook[LevelManager.level]) * lvlmanager.housesToDeliver.Count)))}";
                    points.gameObject.SetActive(true);
                    accuracy.text = $"Accuracy : {((plyrcontroler.nbThrows != 0) ? (((LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count) * 100) / plyrcontroler.nbThrows) : 0)}%";
                    accuracy.gameObject.SetActive(true);
                    bonus.text = $"+ {((plyrcontroler.nbThrows != 0) ? ((500 / 100) * (((LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count) * 100) / plyrcontroler.nbThrows)) : 0)}";
                    bonus.gameObject.SetActive(true);
                    score.text = $"{((LevelManager.level == 10) ? "Final score" : "Score")} : {LevelManager.score}";
                    score.gameObject.SetActive(true);
                }
                if( LevelManager.level == 10)
                {
                    highscore.text = $"High score : {LevelManager.highscore}";
                    highscore.gameObject.SetActive(true);
                }
            }
            if (timer >= 5.5f)
            {
                if (LevelManager.level < 10)
                {
                    curtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 1200);
                    curtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 1200);
                    if (curtains[0].rectTransform.sizeDelta.y >= Screen.height / 2 || curtains[1].rectTransform.sizeDelta.y >= Screen.height / 2)
                    {
                        curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                        curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                        transition = false;
                        timer = 0;
                        if (!toMenu)
                        {
                            if (!toRetry) { LevelManager.level++; }
                            SceneManager.LoadScene(1);
                            CarSpawner.index = 0;
                        }
                        else
                        {
                            SceneManager.LoadScene(0);
                            CarSpawner.index = 0;
                        }
                    }
                }
                else
                {
                    retryButton.gameObject.SetActive(true);
                    menuButton.gameObject.SetActive(true);
                }
            }
        }

        if (toMenu && !lvlup)
        {
            curtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 1200);
            curtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 1200);
            if (curtains[0].rectTransform.sizeDelta.y >= Screen.height / 2 || curtains[1].rectTransform.sizeDelta.y >= Screen.height / 2)
            {
                curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                SceneManager.LoadScene(0);
            }
        }

        delivIG.text = $"{LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count}/{LevelManager.orderBook[LevelManager.level]}";
        if ((LevelManager.orderBook[LevelManager.level] - lvlmanager.housesToDeliver.Count) == LevelManager.orderBook[LevelManager.level] && run)
        {
            delivIG.color = new Color(0.00153816f, 0.5849056f, 0);
            house.color = new Color(0.00153816f, 0.5849056f, 0);
        }
    }

    public void StartButton()
    {
        bckground.gameObject.SetActive(false);
        banner.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        level.gameObject.SetActive(false);
        deliveries.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        delivIG.gameObject.SetActive(true);
        house.gameObject.SetActive(true);
        audiomanager.sndSelect.Play();
        started = true;
    }

    public void RetryButton()
    {
        level.gameObject.SetActive(false);
        deliveries.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        accuracy.gameObject.SetActive(false);
        bonus.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        LevelManager.level = 1;
        LevelManager.score = 0;
        toRetry = true;
        audiomanager.sndSelect.Play();
    }

    public void MenuButton()
    {
        level.gameObject.SetActive(false);
        deliveries.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        accuracy.gameObject.SetActive(false);
        bonus.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        LevelManager.level = 1;
        LevelManager.score = 0;
        toMenu = true;
        audiomanager.sndSelect.Play();
    }
}
