using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDisable : MonoBehaviour {

    TimeControl timecontrol;
    public Image dice;
    public Image bone;
    public Image food;
    public Image spray;
    bool isDelete=false;
    // Use this for initialization
    void Start () {
		timecontrol= GameObject.Find("board").GetComponent<TimeControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timecontrol.start&&!isDelete)
        {
            Destroy(dice.gameObject);
            Destroy(bone.gameObject);
            Destroy(food.gameObject);
            Destroy(spray.gameObject);
            isDelete = true;
        }
           
	}
}
