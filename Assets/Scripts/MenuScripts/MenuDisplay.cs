using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] AudioSource[] lstSnds;
    [SerializeField] Image[] curtains;

    bool transition;
    bool choice;
    bool introPlayed;
    float timer;

    void Start()
    {
        transition = true;
        choice = false;
        introPlayed = false;
        timer = 0;
        curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
        curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
    }

    void Update()
    {
        if (transition)
        {
            timer += Time.deltaTime;

        }
        if (timer >= 0.5 && transition)
        {
            if(!introPlayed) { lstSnds[1].Play(); introPlayed = true; }
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
        if (choice)
        {
            curtains[0].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 1200);
            curtains[1].rectTransform.sizeDelta += new Vector2(0, Time.deltaTime * 1200);
            lstSnds[0].volume -= Time.deltaTime;
            lstSnds[1].volume -= Time.deltaTime*1.5f;
            if (curtains[0].rectTransform.sizeDelta.y >= Screen.height / 2 || curtains[1].rectTransform.sizeDelta.y >= Screen.height / 2)
            {
                curtains[0].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                curtains[1].rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 2);
                SceneManager.LoadScene(1);
                CarSpawner.index = 0;
            }
        }
    }

    public void PlayButton()
    {
        choice = true;
        lstSnds[2].Play();
    }

    public void ExitButton()
    {
        lstSnds[2].Play();
        Application.Quit();
    }
}
