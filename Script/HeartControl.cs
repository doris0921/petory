using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartControl : MonoBehaviour {

    Image heart1;
    public Sprite heart;
    public Sprite heart_line;
    TimeControl timer;

    void Start () {
        heart1 = GetComponent<Image>(); //Our image component is the one attached to this gameObject.
        timer = GameObject.Find("board").GetComponent<TimeControl>();
        
    }
	
	void Update () {
		if(timer.fillAmount<10)
        {
            heart1.sprite = heart_line;
        }
        else
            heart1.sprite = heart;

    }
}
