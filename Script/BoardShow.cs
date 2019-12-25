using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardShow : MonoBehaviour {

    public Text timeText;
    public Text ScoreText;
    TimeControl TimeControl;

	void Start () {
        TimeControl = GameObject.Find("board").GetComponent<TimeControl>();
    }
	
	void Update () {
        timeText.text = "Time：" + TimeControl.time.ToString("F");
        ScoreText.text = TimeControl.score.ToString();
    }
}
