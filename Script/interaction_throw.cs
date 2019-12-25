using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class interaction_throw : MonoBehaviour {

    public Transform dice;  //骰子
    public Transform player;	//玩家
    public Transform dogFood;   //食物
    public Transform dogBowl;	//狗碗
    public Transform bone1;   //骨頭
    public Transform bone2;   //骨頭2
    public Transform bone3;   //骨頭3
    public Transform spray;   //骨頭3
    public GameObject Food;
    public GameObject poo;
    GameObject newFood;
    GameObject newPoo;
    

    public bool bone1Flag = false;
    public bool bone2Flag = false;
    public bool bone3Flag = false;
    public bool foodFlag = false;
    public bool diceFlag = false;
    public bool sprayFlag = false;
    public bool addScore = false;
    public bool startEat = false;
    public bool GoToPlayer = false;
    bool Trigger=false;//丟準了開始吃
    bool haveFood = false;
    bool playedSound = false;
    public bool havePoo = false;
    float recX2, recY2, recZ2;//骰子
    float recfX, recfY, recfZ;//食物
    float recX, recY, recZ;//骨頭
    float recb2X, recb2Y, recb2Z;//骨頭2
    float recb3X, recb3Y, recb3Z;//骨頭3
    float recsX, recsY, recsZ;//噴罐
    //The radius(?
    public float Radius = 0.5f;
    float stop, eating, reset;
    AudioSource sound;
    public AudioClip success;
    public AudioClip fail;
    TimeControl score;
    AnimateControl ani;
    PathFllowing PathFllowing;
    movement movement;
    // Use this for initialization
    void Start () {
        recX2 = dice.position.x;
        recY2 = dice.position.y;
        recZ2 = dice.position.z;
        recfX = dogFood.position.x;
        recfY = dogFood.position.y;
        recfZ = dogFood.position.z;
        recX = bone1.position.x;
        recY = bone1.position.y;
        recZ = bone1.position.z;
        recb2X = bone2.position.x;
        recb2Y = bone2.position.y;
        recb2Z = bone2.position.z;
        recb3X = bone3.position.x;
        recb3Y = bone3.position.y;
        recb3Z = bone3.position.z;
        recsX = spray.position.x;
        recsY = spray.position.y;
        recsZ = spray.position.z;

        score = GameObject.Find("board").GetComponent < TimeControl>();
        ani = GameObject.Find("Dog").GetComponent<AnimateControl>();
       PathFllowing = GameObject.Find("Dog").GetComponent<PathFllowing>();
        sound = GetComponent<AudioSource>();
        movement = GameObject.Find("player").GetComponent<movement>();
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(havePoo); 
        if (!movement.move)
        {
            StartChace();
    }
        toilet();
        if (diceFlag)
        {
            diceFunction();
        }
        else if (foodFlag)
        {
            foodFunction();
        }
        else if (bone1Flag)
        {
            bone1Function();
        }
        else if (bone2Flag)
        {
            bone2Function();
        }
        else if (bone3Flag)
        {
            bone3Function();
        }
        else if (sprayFlag)
        {
            sprayFunction();
        }
        
    }

    void StartChace()
    {
        if ((dice.position.y - recY2) > 0.35f && diceFlag == false)
        {           
           // print("骰子旗標要開了！");
            diceFlag = true;
            addScore = true;
        }
        else if ((dogFood.position.y - recfY) > 0.1f && foodFlag == false)
        {
          //  Debug.Log("foodFlag打開");
            foodFlag = true;
            addScore = true;
        }
        else if ((spray.position.y - recsY) > 0.1f && sprayFlag == false)
        {
          //  Debug.Log("sprayFlag打開");
            sprayFlag = true;
        }
        else if ((bone1.position.y - recY) > 0.3f && bone1Flag == false)
        {
            bone1Flag = true;
            addScore = true;
        }
        else if ((bone2.position.y - recb2Y) > 0.3f && bone2Flag == false)
        {
            bone2Flag = true;
            addScore = true;
        }
        else if ((bone3.position.y - recb3Y) > 0.3f && bone3Flag == false)
        {
            bone3Flag = true;
            addScore = true;
        }

        throwWrong();
    }

    void diceFunction()
    {

        if(PathFllowing.startAction)
        {        
            if (addScore&&score.start)
            {
                sound.PlayOneShot(success, 0.7F);
                score.fillAmount += 6;
                addScore = false;
            }
            stop += Time.deltaTime;
            if (stop > 3.0f)
            {
                GoToPlayer = true;
                dice.localPosition = new Vector3(4.5f, 0.015f, 4.5f);
                if (stop > 6.0f)
                {
                    dice.localPosition = new Vector3(-0.3944f, 0.015f, -0.122f);
                    stop = 0;
                    // corgi.stoppingDistance = 1.0f;
                 //   dog.navMeshAgent.isStopped = false;
                    diceFlag = false;
                    GoToPlayer = false;
                    PathFllowing.startAction = false;
                    PathFllowing.path.Reset();
                }
            }
        }
    }

    void foodFunction()
    {
        if (isThrow())
            Trigger = true;
        if (Trigger)
        {
            if (PathFllowing.startAction)
            {
                if (haveFood == false)
                {
                    dogFood.localPosition = new Vector3(-0.346f, 0f, 0.138f);
                    newFood = Instantiate(Food, new Vector3(dogBowl.position.x+0.04f, dogBowl.position.y+0.1f, dogBowl.position.z+0.23f), Quaternion.identity);//Quaternion.identity-->不能轉向
                    haveFood = true;
                    startEat = true;
                }
                if (startEat)
                {
                    eating += Time.deltaTime;
                    if (addScore && score.start)
                    {
                        sound.PlayOneShot(success, 0.7F);
                      //  Debug.Log("吃東西加分~~");
                        score.fillAmount += 4;
                        addScore = false;
                    }
                    if (eating > 3.0f)
                    {
                        Destroy(newFood);
                        eating = 0;
                        haveFood = false;
                        foodFlag = false;
                        startEat = false;
                        Trigger = false;
                        PathFllowing.startAction = false;
                        PathFllowing.path.Reset();
                    }
                }
            }
        }
        else /*if (dogFood.position.y < 0.1f|| (dogFood.localPosition.x < -0.52f&& dogFood.localPosition.x > -0.628f))*/
        {
            if (!playedSound) { 
          //  Debug.Log("播音樂吵死你w");
            sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
            reset += Time.deltaTime;
            if (reset > 2)
            {
              //  Debug.Log("重置喔");
                dogFood.localPosition = new Vector3(-0.346f, 0f, 0.138f);
                reset = 0;
                playedSound = false;
                foodFlag = false;
                PathFllowing.startAction = false;
                PathFllowing.path.Reset();
                
            }
        }
    }

    bool isThrow()
    {
        float x = Mathf.Abs(dogBowl.position.x - dogFood.position.x);
        float y = Mathf.Abs(dogBowl.position.y - dogFood.position.y);
        float z = Mathf.Abs(dogBowl.position.z - dogFood.position.z);
        if (x < 0.5f && y < 0.5f && z < 0.5f)
        {
          //  Debug.Log("在範圍內");
            return true;
        }
        else
            return false;
    }

    bool isDirty()
    {
        
        float x = Mathf.Abs(poo.transform.position.x - spray.position.x);
        // float y = Mathf.Abs(poo.transform.position.y - spray.position.y);
        float z = Mathf.Abs(poo.transform.position.z - spray.position.z);
        if (x < 0.8f && z < 0.8f)
        {
           // Debug.Log("在範圍內");
            return true;
        }
        else
        {
          //  Debug.Log("不在範圍內");
            return false;
        } 
    }

    void bone1Function()
    {
        if(PathFllowing.startAction)
        {
            startEat = true;
            if (addScore && score.start)
            {
                sound.PlayOneShot(success, 0.7F);
                score.fillAmount += 8;
             //   Debug.Log("1號加分");
                addScore = false;
            }
            eating += Time.deltaTime;
            if (eating > 5)
            {
                bone1.localPosition = new Vector3(-0.434f, 0.205f, -0.286f);
                eating = 0;
                bone1Flag = false;
                startEat = false;
                PathFllowing.startAction = false;
                PathFllowing.path.Reset();
            }
        }
    }

    void bone2Function()
    {
        if (PathFllowing.startAction)
        {
            startEat = true;
            if (addScore && score.start)
            {
                sound.PlayOneShot(success, 0.7F);
                score.fillAmount += 8;
               // Debug.Log("2號加分");
                addScore = false;
            }
            eating += Time.deltaTime;
            if (eating > 5)
            {
                bone2.localPosition = new Vector3(-0.395f, 0.205f, -0.286f);
                eating = 0;
                bone2Flag = false;
                startEat = false;
                PathFllowing.startAction = false;
                PathFllowing.path.Reset();
            }
        }
    }

    void bone3Function()
    {
        if (PathFllowing.startAction)
        {
            startEat = true;
            if (addScore && score.start)
            {
                sound.PlayOneShot(success, 0.7F);
                score.fillAmount += 8;
              //  Debug.Log("3號加分");
                addScore = false;
            }
            eating += Time.deltaTime;
            if (eating > 5)
            {
                bone3.localPosition = new Vector3(-0.486f, 0.205f, -0.286f);
                eating = 0;
                bone3Flag = false;
                startEat = false;
                PathFllowing.startAction = false;
                PathFllowing.path.Reset();
            }
        }
    }

    void sprayFunction()
    {
        if (isDirty())
            Trigger = true;
        if (Trigger)
        {
            if (havePoo == true)
            {
                spray.localPosition = new Vector3(-0.398f, -0.059f, 0.319f);
                addScore = true;
                havePoo = false;
              //  ani.stretchFlag = true;
                
            }
            if (addScore && score.start)
            {
                sound.PlayOneShot(success, 0.7F);
             //   Debug.Log("清潔加分~~");
                score.fillAmount += 5;
                addScore = false;
            }
            reset += Time.deltaTime;
            if (reset > 1.0f)
            {
                Destroy(newPoo);
                reset = 0;
                sprayFlag = false;
                Trigger = false;
                PathFllowing.startAction = false;
                PathFllowing.path.Reset();
            }
        }

        else if (spray.localPosition.y < -0.15f)
        {
          //  Debug.Log("spray reset");

            if (!playedSound)
            {
            //    Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
            reset += Time.deltaTime;
            if (reset > 0.5f)
            {
                spray.localPosition = new Vector3(-0.398f, -0.059f, 0.319f);
                reset = 0;
                sprayFlag = false;
                playedSound = false;
            }
        }
    }

    void throwWrong()
    {
        if ((dice.localPosition.y < -0.116f ||dice.localPosition.x<-0.5f)&& diceFlag == false)
        {
            if (!playedSound)
            {
               // Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
            reset += Time.deltaTime;
            if (reset > 2)
            {
                dice.localPosition = new Vector3(-0.3944f, 0.015f, -0.122f);
                reset = 0;
                playedSound = false;
            }
        }
        if ((dogFood.localPosition.y < -0.116f || dogFood.localPosition.x < -0.52f) && foodFlag == false)
        {
            if (!playedSound)
            {
               // Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
            
            reset += Time.deltaTime;
            if (reset > 2)
            {
                dogFood.localPosition = new Vector3(-0.346f, 0f, 0.138f);
                reset = 0;
                playedSound = false;
                PathFllowing.startAction = false;
                PathFllowing.path.Reset();
            }
        }
        if ((spray.position.y < -0.15f || spray.localPosition.x < -0.5f) && sprayFlag == false)
        {
            if (!playedSound)
            {
              //  Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
            reset += Time.deltaTime;
            if (reset > 2)
            {
                spray.localPosition = new Vector3(-0.398f, -0.059f, 0.319f);
                reset = 0;
                playedSound = false;
            }
        }
        if (bone1.localPosition.y < -0.034f && bone1Flag == false)
        {
            if (!playedSound)
            {
              //  Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
            reset += Time.deltaTime;
            if (reset > 2)
            {
                bone1.localPosition = new Vector3(-0.434f, 0.205f, -0.286f);
                reset = 0;
                playedSound = false;
            }
        }
        if (bone2.localPosition.y < -0.034f && bone2Flag == false)
        {
            if (!playedSound)
            {
             //   Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
         //   Debug.Log("reset");
            reset += Time.deltaTime;
            if (reset > 2)
            {
                bone2.localPosition = new Vector3(-0.395f, 0.205f, -0.286f);
                reset = 0;
                playedSound = false;
            }
        }
        if (bone3.localPosition.y < -0.034f && bone3Flag == false)
        {
            if (!playedSound)
            {
             //   Debug.Log("播音樂吵死你w");
                sound.PlayOneShot(fail, 0.7F);
                playedSound = true;
            }
          //  Debug.Log("reset");
            reset += Time.deltaTime;
            if (reset > 2)
            {
                bone3.localPosition = new Vector3(-0.486f, 0.205f, -0.286f);
                reset = 0;
                playedSound = false;
            }
        }
    }

    void toilet()
    {
        if (ani.poo)//大便的動畫播放
        {
            if (havePoo == false)
            {
              //  Debug.Log("應該大便(?)囉");
                //newPoo = Instantiate(poo, new Vector3(0,0,0), Quaternion.identity);//Quaternion.identity-->不能轉向
                newPoo = Instantiate(poo, new Vector3(PathFllowing.transform.position.x, PathFllowing.transform.position.y, PathFllowing.transform.position.z), Quaternion.identity);//Quaternion.identity-->不能轉向
                poo.transform.position = PathFllowing.transform.position;
                newPoo.transform.Rotate(-90,0,0);    
                havePoo = true;
            }
            ani.poo = false;
        }

    }
}
