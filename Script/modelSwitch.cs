using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelSwitch : MonoBehaviour {

    interaction_throw flag;

    void Start () {
        flag = GameObject.Find("Dog").GetComponent<interaction_throw>();
    }
	
	void Update () {
        if (flag.GoToPlayer)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
	}
}
