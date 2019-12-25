using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFllowing : MonoBehaviour {

    public DogPath path;//The path

    public float speed = 20.0f;//following speed
    public float mass = 5.0f;//this is for object mass for simulating the real car
    bool isLooping = true;//the car will loop or not
    bool followPlayer = false;
    public bool startAction=false;
    public bool Sit = false;
    public bool Sleep = false;
    private float curSpeed;//Actual speed of the car
    private int curPathIndex;
    private float pathLength;
    public Vector3 targetPosition;

    private Vector3 curVelocity;

    interaction_throw dog;
    TimeControl feeling;

    void Start()
    {

        pathLength = path.Length;

        curPathIndex = 0;

        //get the current velocity of the vehicle
        curVelocity = transform.forward;
        dog = GameObject.Find("Dog").GetComponent<interaction_throw>();
        feeling = GameObject.Find("board").GetComponent<TimeControl>();
    }

    void Update()
    {
        //Unify the speed
        curSpeed = speed * Time.deltaTime;
        if (dog.diceFlag)
        {
            targetPosition.x = dog.dice.position.x;
            targetPosition.z = dog.dice.position.z;
            if (dog.GoToPlayer)
            {
                targetPosition.x = dog.player.position.x;
                targetPosition.z = dog.player.position.z;
            }
        }
        else if (dog.bone1Flag)
        {
            targetPosition.x = dog.bone1.position.x;
            targetPosition.z = dog.bone1.position.z;
        }
        else if (dog.bone2Flag)
        {
            targetPosition.x = dog.bone2.position.x;
            targetPosition.z = dog.bone2.position.z;
        }
        else if (dog.bone3Flag)
        {
            targetPosition.x = dog.bone3.position.x;
            targetPosition.z = dog.bone3.position.z;
        }
        else if (dog.foodFlag)
        {
            targetPosition.x = dog.dogBowl.position.x;
            targetPosition.z = dog.dogBowl.position.z;
        }
        else if (feeling.fillAmount >= 60)
        {
            targetPosition.x = dog.player.position.x;
            targetPosition.z = dog.player.position.z;
            speed = 1;
            Sit = true;
           // Debug.Log("坐下~~~");
        }
        else if (feeling.fillAmount <30)
        {
            Sleep = true;
        }
        else
        {
            targetPosition = path.GetPosition(curPathIndex);
            Sleep = false;
            Sit = false;
        }
        //If reach the radius within the path then move to next point in the path
        if (Vector3.Distance(transform.position, targetPosition) < path.Radius || Vector3.Distance(transform.position, targetPosition) < dog.Radius)
        {
            if (dog.diceFlag || dog.bone1Flag || dog.bone2Flag || dog.bone3Flag || dog.foodFlag)
            {
                startAction = true;
            }
            //Don't move the vehicle if path is finished
            else if (curPathIndex < pathLength - 1) {                 
            curPathIndex++;//目標點的數目(猜測
            }
            else if (isLooping)
            curPathIndex = 0;
            else
                return;
        }
        else {//到位置後不讓他動(因為這個是控制加速度
              //Calculate the acceleration towards the path
            curVelocity += Accelerate(targetPosition);

            //Move the car according to the velocity
            transform.position += curVelocity;
            //Rotate the car towards the desired Velocity
            transform.rotation = Quaternion.LookRotation(curVelocity);
        }
    }

    //Steering algorithm to steer the vector towards the target
    public Vector3 Accelerate(Vector3 target)
    {

        //Calculate the directional vector from the current position towards the target point
        Vector3 desiredVelocity = target - transform.position;

        //Normalise the desired Velocity
        desiredVelocity.Normalize();

        desiredVelocity *= curSpeed;

        //Calculate the force Vector
        Vector3 steeringForce = desiredVelocity - curVelocity;
        Vector3 acceleration = steeringForce / mass;

        return acceleration;

    }

    bool Flags()
    {
        if (dog.diceFlag || dog.bone1Flag || dog.bone2Flag || dog.bone3Flag || dog.foodFlag)
            return true;
        else
            return false;
    }
}
