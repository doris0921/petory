using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimeControl : MonoBehaviour
{
    //public Image fillImg;
    float timeAmt = 300;
    public float time;
    public float score;
    public float fillAmount=30;
    public bool start=false;
    public Text timeText;
    public Text ScoreText;
    public Canvas end;
    AudioSource sound;
    public AudioClip bell;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        time = timeAmt;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C)) start = true;

        if (time > 0&&start)
        {
            time -= Time.deltaTime;
            if (time <= 240 && time > 239)
            {
                fillAmount -= 0.06f;
            }
            if (time <= 180 && time > 179)
            {
                fillAmount -= 0.06f;
            }
            if (time <= 120 && time > 119)
            {
                fillAmount -= 0.06f;
            }
            if (time <= 60 && time > 59)
            {
                fillAmount -= 0.06f;
            }
            if (time <= 1 && time > 0)
            {
                fillAmount -= 0.06f;
            }
            score = fillAmount;
            timeText.text = "Time：" + time.ToString("F");
            ScoreText.text = score.ToString();
            if (time < 30 && time > 29)
            {
                sound.PlayOneShot(bell, 0.7F);
            }
             if (Input.GetKeyDown(KeyCode.E))
            {
                if (fillAmount > 60f && start)
                    SceneManager.LoadScene("End");
                else if (fillAmount >= 30f && start)
                    SceneManager.LoadScene("End2");
                else if (fillAmount < 30f && start)
                    SceneManager.LoadScene("End3");
            }
        }
        else
        {
            if (fillAmount >60f &&start)
                SceneManager.LoadScene("End");
            else if (fillAmount >= 30f && start)
                SceneManager.LoadScene("End2");
            else if(fillAmount < 30f && start)
                SceneManager.LoadScene("End3");
        }
    }
}
