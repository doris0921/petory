using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateControl : MonoBehaviour
{
    public Animator Anime;
    public AnimatorStateInfo BS;
    static int eat = Animator.StringToHash("Base Layer.eat");
    static int walk = Animator.StringToHash("Base Layer.walk");
    static int sleep = Animator.StringToHash("Base Layer.sleep");
    static int stretch = Animator.StringToHash("Base Layer.stretch");
    static int toilet = Animator.StringToHash("Base Layer.toilet");
    public float StretchRange;
    public float SitRange;
    float StretchWait, SitWait;

    public bool stop=false;
    public bool test = false;
    public bool poo = false;
    bool stretchFlag = false;
    bool waitForAnime = false;
    interaction_throw flag;
    TimeControl feeling;
    PathFllowing PathFllowing;

    void Start()
    {
        flag = GameObject.Find("Dog").GetComponent<interaction_throw>();
        feeling = GameObject.Find("board").GetComponent<TimeControl>();
        PathFllowing = GameObject.Find("Dog").GetComponent<PathFllowing>();

        Anime.SetBool("walk", false);
        Anime.SetBool("eat", false);
        Anime.SetBool("sleep", false);
        Anime.SetBool("stretch", false);
        Anime.SetBool("toilet", false);
        Anime.SetBool("player", false);
        Anime.SetBool("sit", false);
    }

    void Update()
    {
        Interaction();
        if (feeling.fillAmount < 30)//不開心狀態
        {
            Sadtime();
        }
        else if (feeling.fillAmount >= 30 && feeling.fillAmount <= 60)//正常狀態
        {
            Normaltime();
        }
        else if (feeling.fillAmount > 60)//開心狀態
        {
            Happytime();
        }      
    }

    void Interaction()
    {
        if (Flags())
        {
            Anime.SetBool("walk", true);
        }
        if (flag.startEat == true)//骨頭+食物
        {
            Anime.SetBool("eat", true);
        }
        else if (flag.GoToPlayer == true)//骰子
        {
            Anime.SetBool("player", true);
        }
        else
        {
            Anime.SetBool("eat", false);
            Anime.SetBool("player", false);
            Anime.SetBool("walk", true);
        }
    }

    void Sadtime()
    {
        Anime.SetBool("stretch", false);
        Anime.SetBool("toilet", false);
        if (feeling.fillAmount <= 40 && !Flags())
        {
            //Debug.Log("睡著");
            Anime.SetBool("sleep", true);
            PathFllowing.speed = 0.1f;
            // Anime.SetBool("walk", true);
        }
        else
        {
            //Debug.Log("回去走路");
            Anime.SetBool("sleep", false);
            PathFllowing.speed = 1;
        }
    }

    void Normaltime()
    {
        Anime.SetBool("walk", true);
        Anime.SetBool("sit", false);
        Anime.SetBool("sleep", false);
        //Debug.Log(StretchRange);
        if (!Flags()&&!flag.havePoo)
        {
            stretchFlag = false;
            StretchRange += Time.deltaTime;
            if (StretchRange > 5)
            {
                Anime.SetBool("toilet", true);
                PathFllowing.speed = 0.01f;
                poo = true;
                waitForAnime = true;
            }
            }
        if (waitForAnime)
        {
            StretchRange += Time.deltaTime;
            if (StretchRange > 13) { 
                StretchRange = 0;
                stretchFlag = true;
                waitForAnime = false;
                PathFllowing.speed = 1;
                Anime.SetBool("toilet", false);
            }
        }
        //Debug.Log(StretchWait);
        if (!Flags() && stretchFlag)
        {
            StretchWait += Time.deltaTime;
            if (StretchWait > 5.0f) { 
                Anime.SetBool("stretch", true);
                PathFllowing.speed = 0.01f;
            }
            if (StretchWait > 8.0f)
            {
                StretchWait = 0;
                //stretchFlag = false;
                PathFllowing.speed = 1;
                Anime.SetBool("stretch", false);
            }
         }
        if (Flags())
        {
            //Debug.Log("動作做到一半互動喔ww");
            StretchRange = 0;
            StretchWait = 0;
            PathFllowing.speed = 1;
            Anime.SetBool("stretch", false);
            Anime.SetBool("toilet", false);
        }
    }
        
    void Happytime()
    {
        Anime.SetBool("stretch", false);
        Anime.SetBool("toilet", false);
        if (PathFllowing.Sit&&!Flags())
        {
           // Debug.Log("坐著");
            Anime.SetBool("sit", true);
            PathFllowing.speed = 0.01f;
            //  Anime.SetBool("walk", false);
        }
        else
        {
          //  Debug.Log("回去走路");
            Anime.SetBool("sit", false);
            PathFllowing.speed = 1;
            // Anime.SetBool("walk", true);
        }

    }

   bool Flags()
    {
        if (flag.diceFlag || flag.bone1Flag || flag.bone2Flag || flag.bone3Flag || flag.foodFlag || flag.sprayFlag)
            return true;
        else
            return false;
    }
}
