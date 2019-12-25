using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("petory4");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("start");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
	}
}
